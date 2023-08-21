Imports MySql.Data.MySqlClient
Imports ggcAppDriver

Public Class frmSample
    Private Const pxeTableName As String = "Sample"

    Private pnLoadx As Integer
    Private poControl As Control

    Private p_nEditMode As Integer

    Dim poRow As DataRow

    Private Sub frmProductSpecs_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            NewRecord()
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmProductSpecs_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If pnLoadx = 0 Then
            'Set event Handler for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            p_nEditMode = xeEditMode.MODE_UNKNOWN
            initButton()

            pnLoadx = 1
        End If
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1
            End Select
        End If

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 3
                        SearchRecord()
                End Select
            End If
        End If
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" Then
            Select Case loIndex
                Case 1
            End Select
        End If

        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 1 'new
                NewRecord()
            Case 2 'save
                SaveRecord()
            Case 3 'search
                SearchRecord()
            Case 4 'browse
                BrowseRecord()
            Case 5 'cancel
                CancelTransaction()
            Case 6 'update
                UpdateRecord()
            Case 7 'del
                DeleteRecord()
            Case 8 'close
                Me.Close()
        End Select
    End Sub

    Private Sub initButton()
        'UNKNOWN = -1
        'READY = 0
        'ADDNEW = 1
        'UPDATE = 2
        'DELETE = 3

        Dim lbShow As Integer
        lbShow = (p_nEditMode = 1 Or p_nEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton03.Visible = lbShow
        cmdButton05.Visible = lbShow
        GroupBox1.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
    End Sub

    Private Function NewRecord() As Boolean
        clearText()

        txtField00.Text = GetNextCode(pxeTableName, "sSpecsIDx", False, p_oAppDriver.Connection, True, p_oAppDriver.BranchCode)

        p_nEditMode = xeEditMode.MODE_ADDNEW
        initButton()

        txtField01.Focus()
        Return True
    End Function

    Private Function SaveRecord() As Boolean
        If Not isEntryOK() Then Return False

        Dim lsSQL As String = ""
        Dim lnRow As Integer

        With p_oAppDriver
            Select Case p_nEditMode
                Case xeEditMode.MODE_ADDNEW
                    lsSQL = "INSERT INTO " & pxeTableName & _
                                "  SET sSpecsIDx = " & strParm(txtField00.Text) & _
                                    ", sBriefDsc = " & strParm(txtField01.Text) & _
                                    ", sDescript = " & strParm(txtField02.Text) & _
                                    ", sCategIDx = " & strParm(txtField03.Tag) & _
                                    ", cRecdStat = " & strParm(xeRecordStat.RECORD_NEW) & _
                                    ", sModified = " & strParm(.UserID) & _
                                    ", dModified = " & dateParm(.SysDate)

                Case xeEditMode.MODE_UPDATE
                    If Not isModified() Then Return True

                    lsSQL = "UPDATE " & pxeTableName & _
                                "  SET sSpecsIDx = " & strParm(txtField00.Text) & _
                                    ", sBriefDsc = " & strParm(txtField01.Text) & _
                                    ", sDescript = " & strParm(txtField02.Text) & _
                                    ", sCategIDx = " & strParm(txtField03.Tag) & _
                                    ", sModified = " & strParm(.UserID) & _
                                    ", dModified = " & dateParm(.SysDate)
            End Select

            .BeginTransaction()
            lnRow = .Execute(lsSQL, pxeTableName)
            If lnRow = 0 Then GoTo endWithroll
            .CommitTransaction()
        End With

        MsgBox("Record Saved Successfuly.", MsgBoxStyle.Information, "Success")

        p_nEditMode = xeEditMode.MODE_READY
        initButton()

        Return True
endwithRoll:
        p_oAppDriver.RollBackTransaction()
        MsgBox("Unable to Save Record. Please verify your entry.", MsgBoxStyle.Critical, "Warning")
        Return False
    End Function

    Private Function DeleteRecord() As Boolean
        Dim lsSQL As String
        Dim lnRow As Integer

        If Not p_nEditMode = xeEditMode.MODE_READY Then Return False
        If MsgBox("Are you sure to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
            Return False
        End If

        lsSQL = "DELETE FROM " & pxeTableName & _
                " WHERE sSpecsIDx = " & strParm(txtField00.Text)


        With p_oAppDriver
            .BeginTransaction()
            lnRow = .Execute(lsSQL, pxeTableName)
            If lnRow = 0 Then GoTo endWithroll
            .CommitTransaction()
        End With

        MsgBox("Record Deleted Successfuly.", MsgBoxStyle.Information, "Success")

        p_nEditMode = xeEditMode.MODE_UNKNOWN
        initButton()
        clearText()

        Return True
endwithRoll:
        p_oAppDriver.RollBackTransaction()
        MsgBox("Unable to Delete Record.", MsgBoxStyle.Critical, "Warning")
        Return False
    End Function

    Private Function BrowseRecord() As Boolean
        Dim lsSQL As String
        Dim lsFilter As String = ""

        lsSQL = "SELECT" & _
                    "  a.sSpecsIDx" & _
                    ", a.sBriefDsc" & _
                    ", a.sDescript" & _
                    ", a.sCategIDx" & _
                    ", b.sBriefDsc" & _
                " FROM " & pxeTableName & " a" & _
                    " LEFT JOIN Product_Specs b" & _
                        " ON a.sCategIDx = b.sCategIDx" & _
                " WHERE a.cRecdStat = " & strParm(xeRecordStat.RECORD_NEW)

        With p_oAppDriver
            poRow = KwikSearch(p_oAppDriver _
                                            , lsSQL _
                                            , False _
                                            , lsFilter _
                                            , "sSpecsIDx»sBriefDsc»sDescript" _
                                            , "Specs ID»Brief Desc»Description", _
                                            , "a.sSpecsIDx»a.sBriefDsc»a.sDescript")

            If Not IsNothing(poRow) Then
                txtField00.Text = poRow(0)
                txtField01.Text = poRow(1)
                txtField02.Text = poRow(2)
                txtField03.Text = poRow(4)
                txtField03.Tag = poRow(3)

                p_nEditMode = xeEditMode.MODE_READY
                initButton()
            Else
                Return False
            End If
        End With

        Return True
    End Function

    Private Function SearchRecord() As Boolean
        Dim lsSQL As String
        Dim lsFilter As String = ""

        lsSQL = "SELECT * FROM Product_Category" & _
                " WHERE cRecdStat = " & strParm(xeRecordStat.RECORD_NEW)

        With p_oAppDriver
            Dim loRow As DataRow = KwikSearch(p_oAppDriver _
                                            , lsSQL _
                                            , False _
                                            , lsFilter _
                                            , "sCategIDx»sBriefDsc»sDescript" _
                                            , "Category ID»Brief Desc»Description")

            If Not IsNothing(loRow) Then
                txtField03.Tag = loRow(0)
                txtField03.Text = loRow(1)
            Else
                Return False
            End If
        End With

        Return True
    End Function

    Private Function UpdateRecord() As Boolean
        If p_nEditMode <> xeEditMode.MODE_READY Then Return False

        p_nEditMode = xeEditMode.MODE_UPDATE
        initButton()

        Return True
    End Function

    Private Function isEntryOK() As Boolean
        If txtField00.Text = "" Or _
           txtField01.Text = "" Or _
           txtField02.Text = "" Or _
           txtField03.Text = "" Or _
           txtField03.Tag = "" Then Return False

        Return True
    End Function

    Private Function isModified() As Boolean
        If p_nEditMode <> xeEditMode.MODE_UPDATE Then Return False
        If IsNothing(poRow) Then Return False

        If poRow(0) <> txtField00.Text Or _
            poRow(1) <> txtField01.Text Or _
            poRow(2) <> txtField02.Text Or _
            poRow(4) <> txtField03.Tag Then Return True

        Return False
    End Function

    Private Function CancelTransaction() As Boolean
        clearText()

        p_nEditMode = xeEditMode.MODE_UNKNOWN
        initButton()

        Return True
    End Function

    Private Sub clearText()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField02.Text = ""
        txtField03.Text = ""
        txtField02.Tag = ""
    End Sub
End Class
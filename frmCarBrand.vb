Imports MySql.Data.MySqlClient
Imports ggcAppDriver

Public Class frmCarBrand
    Private Const pxeTableName As String = "Car_Brand"

    Private pnLoadx As Integer
    Private poControl As Control

    Private p_nEditMode As Integer
    Dim loRow As DataRow
    Dim poRow As DataRow

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            NewRecord()
            txtField01.Focus()
            pnLoadx = 2
        End If

    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Up, Keys.Down
                Select Case e.KeyCode
                    Case Keys.Return, Keys.Down
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
    Private Sub initButton()
        'UNKNOWN = -1
        'READY = 0
        'ADDNEW = 1
        'UPDATE = 2
        'DELETE = 3

        Dim lbShow As Integer
        lbShow = (p_nEditMode = 1 Or p_nEditMode = 2)

        cmdButton02.Visible = lbShow
        cmdButton05.Visible = lbShow
        GroupBox1.Enabled = lbShow

        cmdButton01.Visible = Not lbShow
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
    End Sub


    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
         Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub


    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
          Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        loTxt.BackColor = SystemColors.Window
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 1 'new
                NewRecord()
                txtField01.Focus()
            Case 2 'save
                SaveRecord()
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

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub


    Private Function NewRecord() As Boolean
        clearText()
        txtField00.Text = GetNextCode(pxeTableName, "sBrandIDx", False, p_oAppDriver.Connection, True, p_oAppDriver.BranchCode)
        p_nEditMode = xeEditMode.MODE_ADDNEW
        initButton()
        Return True
    End Function

    Private Function SaveRecord() As Boolean
        If DataComplete() Then
            Dim hbStr As String
            If chbActive.Checked Then
                hbStr = "1"
            Else
                hbStr = "0"
            End If

            If Not isEntryOK() Then Return False

            Dim lsSQL As String = ""
            Dim lnRow As Integer

            With p_oAppDriver
                Select Case p_nEditMode
                    Case xeEditMode.MODE_ADDNEW
                        lsSQL = "INSERT INTO " & pxeTableName & " SET" & _
                                        "  sBrandIDx = " & strParm(txtField00.Text) & _
                                        ", sBrandNme = " & strParm(txtField01.Text) & _
                                        ", cRecdStat = " & strParm(hbStr) & _
                                        ", sModified = " & strParm(.UserID) & _
                                        ", dModified = " & dateParm(.SysDate)

                    Case xeEditMode.MODE_UPDATE
                        If Not isModified() Then Return True

                        lsSQL = "UPDATE " & pxeTableName & " SET" & _
                                        " sBrandIDx = " & strParm(txtField00.Text) & _
                                        ", sBrandNme = " & strParm(txtField01.Text) & _
                                        ", cRecdStat = " & strParm(hbStr) & _
                                        ", sModified = " & strParm(.UserID) & _
                                        ", dModified = " & dateParm(.SysDate) & _
                                " WHERE sBrandIdx = " & strParm(txtField00.Text)
                End Select

                .BeginTransaction()
                lnRow = .Execute(lsSQL, pxeTableName)
                If lnRow = 0 Then GoTo endWithroll
                .CommitTransaction()
            End With

            MsgBox("Record Saved Successfuly.", MsgBoxStyle.Information, "Success")
            Call clearText()

            p_nEditMode = xeEditMode.MODE_READY
            initButton()

            Return True
endwithRoll:
            p_oAppDriver.RollBackTransaction()
            MsgBox("Unable to Save Record. Please verify your entry.", MsgBoxStyle.Critical, "Warning")
            Return False
        End If

    End Function

    Private Function DeleteRecord() As Boolean
        Dim lsSQL As String
        Dim lnRow As Integer
        Dim hbStr As String
        chbActive.Checked = False
        If chbActive.Checked Then
            hbStr = "1"
        Else
            hbStr = "0"
        End If

        If Not p_nEditMode = xeEditMode.MODE_READY Then Return False
        If MsgBox("Are you sure to delete this record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then
            chbActive.Checked = True
            Return False
        End If
        With p_oAppDriver

            'lsSQL = "DELETE FROM " & pxeTableName & _
            '       " WHERE sBrandIDx = " & strParm(txtField00.Text)
            lsSQL = "UPDATE " & pxeTableName & " SET" & _
                                              " sBrandIDx = " & strParm(txtField00.Text) & _
                                              ", sBrandNme = " & strParm(txtField01.Text) & _
                                              ", cRecdStat = " & strParm(hbStr) & _
                                              ", sModified = " & strParm(.UserID) & _
                                              ", dModified = " & dateParm(.SysDate) & _
                                      " WHERE sBrandIdx = " & strParm(txtField00.Text)
        End With
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

    Private Function BrowseRecord()
        Dim lsSQL As String
        Dim lsFilter As String = ""

        lsSQL = "SELECT" & _
                    "  sBrandIDx" & _
                    ", sBrandNme" & _
                    ", cRecdStat" & _
                " FROM " & pxeTableName & _
                " WHERE cRecdStat = 1 " & _
                " AND sBrandNme LIKE " & strParm(txtField01.Text + "%")

        With p_oAppDriver
            poRow = KwikSearch(p_oAppDriver _
                                            , lsSQL _
                                            , True _
                                            , "" _
                                            , "sBrandIDx»sBrandNme" _
                                            , "Brand ID»Brand Name")

            If Not IsNothing(poRow) Then
                txtField00.Text = poRow(0)
                txtField01.Text = poRow(1)
                txtField00.Tag = poRow(0)
                txtField01.Tag = poRow(1)
                chbactive.Checked = poRow(2)

                p_nEditMode = xeEditMode.MODE_READY
                initButton()
            Else
                txtField01.Text = ""
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
           txtField01.Text = "" Then Return False

        Return True
    End Function

    Private Function isModified() As Boolean
        If p_nEditMode <> xeEditMode.MODE_UPDATE Then Return False
        If IsNothing(poRow) Then Return False

        If poRow(1) <> txtField01.Text Then Return True
        If poRow(0) <> txtField00.Text Then Return True
        Return False
    End Function

    Private Function CancelTransaction() As Boolean
        If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.Information, "Car Brand Entry") Then
        End If
        clearText()

        p_nEditMode = xeEditMode.MODE_UNKNOWN
        initButton()

        Return True
    End Function

    Private Sub clearText()
        txtField00.Text = ""
        txtField01.Text = ""
        txtField01.Tag = ""
    End Sub

    Private Function DataComplete() As Boolean
        If txtField00.Text = "" Then
            MessageBox.Show("Please Input Brand ID", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField00
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField01.Text = "" Then
            MessageBox.Show("Please Input Brand Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField01
                .Focus()
                .SelectAll()
            End With
            Return False
        End If
        Return True
    End Function
End Class
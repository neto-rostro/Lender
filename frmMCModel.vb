Imports ggcAppDriver
Imports MySql.Data.MySqlClient
Imports ggcLRTransaction
Imports System.Globalization

Public Class frmMCModel
    Private WithEvents p_oTrans As ggcLRTransaction.McModel
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer

    Private Sub frmMCModel_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMCModel_Activated")
        If pnLoadx = 1 Then
            p_oTrans.InitTransaction()
            p_oTrans.NewTransaction()
            Call newRecord()
            p_nEditMode = xeEditMode.MODE_ADDNEW
            pnLoadx = 2
        End If
    End Sub

    Private Function newRecord() As Boolean
        Call loadMaster()
        txtSeeks00.Focus()
        initFeature()
        initSpecs()
        Return True
    End Function

    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Right
                        SetNextFocus()
                    Case Keys.Up, Keys.Left
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub initFeature()
        'this will add items to list view.
        With ListView2
            .Columns.Clear()
            .Items.Clear()

            ListView2.View = View.Details
            .Columns.Add("No", 50, HorizontalAlignment.Center)
            .Columns.Add("Feature", 125, HorizontalAlignment.Center)

        End With

    End Sub

    Private Sub initSpecs()
        'this will add items to list view.
        With ListView1
            .Columns.Clear()
            .Items.Clear()
            ListView1.View = View.Details
            .Columns.Add("No", 50, HorizontalAlignment.Center)
            .Columns.Add("Specs", 125, HorizontalAlignment.Center)

        End With

    End Sub

    Private Sub LoadFeature()
        Dim lnCtr As Integer
        With ListView2
            .Items.Clear()
            For lnCtr = 0 To p_oTrans.ItemCount - 1
                .Items.Add(p_oTrans.Detail(lnCtr, "nEntryNox")).SubItems.Add(p_oTrans.Detail(lnCtr, "sDescript"))
            Next
        End With
    End Sub

    Private Sub LoadSpecs()
        Dim lnCtr As Integer
        With ListView1
            .Items.Clear()
            For lnCtr = 0 To p_oTrans.OthersCount - 1
                .Items.Add(p_oTrans.Others(lnCtr, "nEntryNox")).SubItems.Add(p_oTrans.Others(lnCtr, "sDescript"))
            Next
        End With
    End Sub

    Private Sub frmMCModel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            If p_oTrans.SearchTransaction(poControl.Text) = True Then
                                loadMaster()
                                LoadFeature()
                                LoadSpecs()
                            Else
                                clearText()
                            End If

                        End If
                End Select
            End If
            '###########################
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmCarSerial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCarSerial_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.McModel(p_oAppDriver)
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf frmMCModel_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster()
        loadEntry(GroupBox1)
    End Sub

    Private Sub clearText()
        txtSeeks00.Text = ""
        txtField00.Text = ""
        txtField01.Text = ""
        initFeature()
        initSpecs()
    End Sub

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 0
                            loTxt.Text = p_oTrans.Master("sModelCde")
                        Case 1
                            loTxt.Text = p_oTrans.Master("sModelNme")
                    End Select
                    p_nEditMode = xeEditMode.MODE_READY
                End If
            End If
        Next
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))
        Select Case lnIndex
            Case 1, 2 ' Close, Ok
                Me.Dispose()
            Case 0 ' Search
                If p_oTrans.SearchTransaction(txtSeeks00.Text) = True Then
                    loadMaster()
                    LoadFeature()
                    LoadSpecs()
                Else
                    clearText()
                End If
        End Select
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeeks00.GotFocus
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeeks00.LostFocus
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
    End Sub

    Private Sub p_oTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.MasterRetrieved
        Dim loTxt As TextBox
        'ind TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)
        Select Case Index
            Case 0
                loTxt.Text = p_oTrans.Master("sModelCde")
            Case 1
                loTxt.Text = p_oTrans.Master("sModelNme")
        End Select
    End Sub
End Class
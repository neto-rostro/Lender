Imports ggcAppDriver
Imports MySql.Data.MySqlClient
Imports ggcLRTransaction
Imports System.Globalization

Public Class frmRedeemableItems
    Private WithEvents p_oTrans As RedeemableItem
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer

    Private Sub frmRedeemableItems_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmRedeemableItems_Activated")
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

    Private Sub frmRedeemableItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                            If loTxt.Text = "" Then
                                MsgBox("Please input a text to search! ", MsgBoxStyle.Critical, "Redeemable Item")
                                Exit Sub
                            End If

                            If p_oTrans.SearchTransaction(poControl.Text, False) = True Then
                                loadMaster()
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

    Private Sub frmRedeemableItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmRedeemableItems_Load")
        If pnLoadx = 0 Then
            p_oTrans = New RedeemableItem(p_oAppDriver)
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf frmRedeemableItems_KeyDown)
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
        txtField02.Text = "0.0"
        chk00.Checked = False
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
                            loTxt.Text = p_oTrans.Master("sPromCode")
                        Case 1
                            loTxt.Text = p_oTrans.Master("sPromDesc")
                        Case 2
                            loTxt.Text = p_oTrans.Master("nPointsxx")
                    End Select
                End If
            End If
            txtSeeks00.Text = p_oTrans.Master("sPromDesc")
            chk00.Checked = IIf(p_oTrans.Master("cPreOrder") = "0", False, True)
            p_nEditMode = xeEditMode.MODE_READY

        Next
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))
        Select Case lnIndex
            Case 1 'Ok
                If p_oTrans.Master("cPreOrder") = IIf(chk00.Checked = True, "1", "0") Then
                    MsgBox("No record modification! ", MsgBoxStyle.Critical, "Redeemable Item")
                    Exit Sub
                End If

                p_oTrans.Master("cPreOrder") = IIf(chk00.Checked = True, "1", "0")
                If p_oTrans.SaveUpdate(p_oTrans.Master("sPromCode")) = True Then
                    MsgBox("Transaction successfully updated!", MsgBoxStyle.Information, "Redeemable Item")
                Else
                    MsgBox("Unable to update transaction!", MsgBoxStyle.Critical, "Redeemable Item")
                End If

            Case 2 ' Close 
                Me.Dispose()
            Case 0 ' Search
                If txtSeeks00.Text = "" Then
                    MsgBox("Please input a text to search! ", MsgBoxStyle.Critical, "Redeemable Item")
                    Exit Sub
                End If

                If p_oTrans.SearchTransaction(txtSeeks00.Text, False) = True Then
                    loadMaster()
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
                loTxt.Text = p_oTrans.Master("sPromCode")
            Case 1
                loTxt.Text = p_oTrans.Master("sPromDesc")
            Case 2
                loTxt.Text = p_oTrans.Master("nPointsxx")
        End Select
        chk00.Checked = p_oTrans.Master("cPreOrder")
    End Sub
End Class
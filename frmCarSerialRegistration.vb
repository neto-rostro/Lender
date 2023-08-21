Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcLRTransaction
Public Class frmCarSerialRegistration

    Private WithEvents p_oTrans As ggcLRTransaction.LRCarSerialRegistration
    Private pnLoadx As Integer
    Private poControl As Control
   

    Private Sub frmCarSerial_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCarSerial_Activated")

        If pnLoadx = 1 Then
            ' If p_oTrans.NewTransaction() Then
            Call newRecord()
            Call showEditMode(Me, xeEditMode.MODE_ADDNEW)

            txtField01.Focus()
        End If


        pnLoadx = 2
        '   End If

    End Sub

    Private Function newRecord() As Boolean
        Return True
    End Function

    Private Sub frmCarSerialEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As TextBox
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            '*********************
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 4, 5
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            Call p_oTrans.SearchMaster(loIndex, loTxt.Text & "%")
                            If txtField04.Text <> "" Then SetNextFocus()
                            If txtField05.Text <> "" Then SetNextFocus()
                        Else
                            If txtField04.Text Or txtField05.Text <> "" Then p_oTrans.SearchMaster(txtField01.Text, False)
                        End If
                End Select
            End If

        End If
    End Sub

    Private Sub frmCarSerialEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCarSerial_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.LRCarSerialRegistration(p_oAppDriver)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            '   Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf frmCarSerialEntry_KeyDown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster(ByVal loControl As Control)
        Dim loTxt As Control

        For Each loTxt In loControl.Controls
            If (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 1
                            If IsDate(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                            Else
                                loTxt.Text = ""
                            End If
                        Case Else
                            loTxt.Text = p_oTrans.Master(loIndex)
                    End Select
                End If
            End If
        Next
    End Sub

    Private Sub showEditMode(ByVal loControl As Control, ByVal fnEditMode As xeEditMode)
        Dim loBtn As Control

        For Each loBtn In loControl.Controls
            If loBtn.HasChildren Then
                Call showEditMode(loBtn, fnEditMode)
            Else
                If (TypeOf loBtn Is Button) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loBtn.Name, 10))
                    If LCase(Mid(loBtn.Name, 1, 9)) = "cmdbutton" Then
                        Select Case fnEditMode
                            Case xeEditMode.MODE_ADDNEW, xeEditMode.MODE_UPDATE
                                loBtn.Visible = IIf(loIndex = 0 Or loIndex = 1 Or loIndex = 5, True, False)
                            Case Else
                                loBtn.Visible = IIf(loIndex = 0 Or loIndex = 1 Or loIndex = 5, False, True)
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 9)) = "cmdButton"
                ElseIf (TypeOf loBtn Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loBtn.Name, 9))
                    If loBtn.TabStop Then
                        Select Case fnEditMode
                            Case xeEditMode.MODE_ADDNEW, xeEditMode.MODE_UPDATE
                                loBtn.Enabled = IIf(LCase(Mid(loBtn.Name, 1, 8)) = "txtfield", True, False)
                            Case Else
                                loBtn.Enabled = IIf(LCase(Mid(loBtn.Name, 1, 8)) = "txtfield", False, True)
                        End Select
                    End If
                End If '(TypeOf loTxt Is Button)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls
    End Sub
    'Handles GotFocus Events for txtField & txtField
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
    End Sub

    'Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            p_oTrans.Master(loIndex) = loTxt.Text
            If loIndex = 1 Then
                txtField01.Text = p_oTrans.Master(1)
            End If
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 1 ' Ledger
                'Dim loFrm As New frmCarRegistrationLedger
                'loFrm.SerialID = p_oTrans.Master("sSerialID")
                'loFrm.ClientName = p_oTrans.Master("sClientID")
                'frmCarRegistrationLedger.ShowDialog()
            Case 2 ' Search
                'Dim loIndex As Integer
                'loIndex = Val(Mid(poControl.Name, 10))

                'If loIndex = 4 Or 5 Then
                '    p_oTrans.SearchMaster(loIndex, poControl.Text & "%")
                'Else
                '    If txtField04.Text <> "" Then p_oTrans.SearchMaster(txtField02.Text, False)
                '    If txtField05.Text <> "" Then p_oTrans.SearchMaster(txtField05.Text, False)
                'End If
            Case 3 ' Cancel Update
                'If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.Information, "Car Serial Entry") Then
                '    'p_oTrans.NewTransaction()
                '    Call newRecord()
                '    showEditMode(Me, xeEditMode.MODE_READY)
                'End If
            Case 4 'Save confirmation

                'If DataComplete() Then
                '    If p_oTrans.SaveTransaction Then
                '        MsgBox("Car Serial was save successfully!", MsgBoxStyle.Information, "Car Serial Entry")
                '        showEditMode(Me, xeEditMode.MODE_READY)
                '        txtField01.Focus()
                '    Else

                '        MsgBox("Please check your Entry!", MsgBoxStyle.Information, "Car Serial Entry")
                '    End If
                'End If

            Case 5 'Browse
                'Dim loIndex As Integer
                'loIndex = Val(Mid(poControl.Name, 10))

                'If loIndex = 1 Then
                '    p_oTrans.SearchMaster(poControl.Text, True)
                'End If

        End Select
    End Sub


    Private Function DataComplete() As Boolean
        If txtField01.Text = "" Then
            MessageBox.Show("Please enter Date of Registration", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField01
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField02.Text = "" Then
            MessageBox.Show("Please enter client name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField02
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField03.Text = "" Then
            MessageBox.Show("Please enter Co-Buyer", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField03
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField04.Text = "" Then
            MessageBox.Show("Please enter Co-Buyer 2", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField04
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField05.Text = "" Then
            MessageBox.Show("Please enter registered id#1", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField05
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField06.Text = "" Then
            MessageBox.Show("Please enter registered id#2", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField06
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField07.Text = "" Then
            MessageBox.Show("Please enter registered id#3", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField07
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField08.Text = "" Then
            MessageBox.Show("Please enter File No.", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField08
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField09.Text = "" Then
            MessageBox.Show("Please Input CRE No!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField09
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField10.Text = "" Then
            MessageBox.Show("Please Control No!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField10
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField11.Text = "" Then
            MessageBox.Show("Please enter Plate No", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField11
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField12.Text = "" Then
            MessageBox.Show("Please enter registered OR#!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField12
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField13.Text = "" Then
            MessageBox.Show("Please enter sticker No", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField13
                .Focus()
                .SelectAll()
            End With
            Return False
        End If

        Return True
    End Function
End Class

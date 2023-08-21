Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcLRTransaction
Imports System.Globalization

Public Class frmCarSerial
    Private WithEvents p_oTrans As ggcLRTransaction.LRCarSerial
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer

    Private Sub frmCarSerial_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCarSerial_Activated")

        If pnLoadx = 1 Then
            If p_oTrans.NewTransaction() Then
                Call newRecord()
            End If
            pnLoadx = 2
        End If
    End Sub

    Private Function newRecord() As Boolean
        Call loadMaster()
        p_nEditMode = xeEditMode.MODE_ADDNEW
        initButton()
        txtField01.Focus()
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

    Private Sub frmCarSerial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 1
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            If p_nEditMode = xeEditMode.MODE_READY Then
                                If p_oTrans.SearchTransaction(loTxt.Text, False) Then loadMaster()
                            End If
                        End If
                    Case 4, 5
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            Call p_oTrans.SearchMaster(loIndex, loTxt.Text)
                            If txtField05.Text <> "" Then SetNextFocus()
                        Else
                            If txtField04.Text Or txtField05.Text <> "" Then p_oTrans.SearchMaster(txtField01.Text, False)
                        End If
                    Case 16 To 21
                        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
                            Call p_oTrans.SearchMaster(loIndex, loTxt.Text)
                            If loTxt.Text <> "" Then SetNextFocus()
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
            p_oTrans = New ggcLRTransaction.LRCarSerial(p_oAppDriver)
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf frmCarSerial_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster()
        Call loadEntry(Me.Panel3)
        Call loadEntry(Me.Panel5)
        txtField00.Text = p_oTrans.Master("sSerialID")
        txtField01.Text = p_oTrans.Master("sEngineNo")
        chbStatus.Checked = IIf(p_oTrans.Master("cSoldStat") = "0", False, True)
        cmbLocation.SelectedIndex = CInt(p_oTrans.Master("cLocation").ToString)
    End Sub

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 13
                            If IsDate(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                            End If
                        Case 6
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "")
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Master(loIndex), "")
                    End Select
                End If
            End If
        Next
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
        cmdButton04.Visible = lbShow
        Panel3.Enabled = lbShow
        Panel5.Enabled = lbShow
        Panel1.Enabled = lbShow

        cmdButton00.Visible = Not lbShow
        cmdButton01.Visible = Not lbShow
        cmdbutton06.Visible = Not lbShow
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

    Function isKnownGoodDate(ByVal input As String) As Boolean 'Define the function and its return value.
        Dim yrMin As Integer = 1900
        Dim yrMax As Integer = Format(p_oAppDriver.getSysDate, "yyyy")
        Try
            If (IsNumeric(input)) Then 'Checks if the input is a number
                If (input.Length = 4) Then
                    Dim MyDate As String = "#01/01/" + input + "#"
                    If input >= yrMin And input <= yrMax Then
                        If (IsDate(MyDate)) Then
                            Return True
                        End If
                    End If
                End If
            End If
        Catch
            Return False
        End Try
    End Function

    'Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            Select Case loIndex
                Case 1, 2
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Master(loIndex) = loTxt.Text
                Case 13
                    If Not IsDate(loTxt.Text) Then
                        loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                        p_oTrans.Master(loIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDate(loTxt.Text), "MMMM dd, yyyy")
                        p_oTrans.Master(loIndex) = loTxt.Text
                    End If
                Case 6
                    If isKnownGoodDate(loTxt.Text) = False Then
                        MsgBox("Incorrect Value of Year Model")
                        loTxt.Text = Year(Now)
                        loTxt.Focus()
                        p_oTrans.Master(loIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CInt(loTxt.Text))
                        p_oTrans.Master(loIndex) = loTxt.Text
                    End If
                Case Else
                    p_oTrans.Master(loIndex) = loTxt.Text
            End Select
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
            Case 1 ' New
                If p_oTrans.NewTransaction Then
                    Call newRecord()
                    p_nEditMode = xeEditMode.MODE_ADDNEW
                    txtField01.Focus()
                    txtField01.Enabled = True
                    txtField02.Enabled = True
                    txtField04.Enabled = True
                    txtField06.Enabled = True
                End If
            Case 2 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))
                If loIndex = 4 Or 5 Then
                    p_oTrans.SearchMaster(loIndex, poControl.Text & "%")
                Else
                    If txtField04.Text <> "" Then p_oTrans.SearchMaster(txtField02.Text, False)
                    If txtField05.Text <> "" Then p_oTrans.SearchMaster(txtField05.Text, False)
                End If
            Case 3 ' Cancel Update
                If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.Information, "Car Serial Entry") Then
                    p_oTrans.NewTransaction()
                    Call newRecord()
                    p_nEditMode = xeEditMode.MODE_READY
                    initButton()
                End If
            Case 4 'Save confirmation
                If chbStatus.Checked Then
                    p_oTrans.Master("cSoldStat") = "1"
                Else
                    p_oTrans.Master("cSoldStat") = "0"
                End If
                p_oTrans.Master("cLocation") = cmbLocation.SelectedIndex.ToString

                If DataComplete() Then
                    If p_oTrans.SaveTransaction Then
                        MsgBox("Car Serial was save successfully!", MsgBoxStyle.Information, "Car Serial Entry")
                        Call p_oTrans.NewTransaction()
                        loadMaster()
                        initButton()
                        txtField01.Focus()
                        txtField01.Enabled = True
                        txtField02.Enabled = True
                        txtField04.Enabled = True
                        txtField06.Enabled = True
                    Else
                        MsgBox("Please check your Entry!", MsgBoxStyle.Information, "Car Serial Entry")
                    End If
                End If
                p_nEditMode = xeEditMode.MODE_ADDNEW
                initButton()
            Case 5 'Browse
                p_oTrans.SearchTransaction("", False)
                loadMaster()
                p_nEditMode = xeEditMode.MODE_READY
                initButton()

            Case 6 'Registration
                'Dim loFrm As New frmCarRegistrationLedger
                'loFrm.SerialID = p_oTrans.Master("sSerialID")
                'loFrm.ClientName = p_oTrans.Master("sClientNm")
                'loFrm.PlateNo = p_oTrans.Master("sPlateNoP")
                'loFrm.ShowDialog()

            Case 7 ' update
                If Not p_oTrans.Master("sSerialID") = "" And Not p_oTrans.Master("sEngineNo") = "" Then
                    If p_oTrans.UpdateTransaction() Then
                        p_nEditMode = xeEditMode.MODE_UPDATE
                        initButton()
                        txtField05.Focus()
                        txtField01.Enabled = False
                        txtField02.Enabled = False
                        txtField04.Enabled = False
                        txtField06.Enabled = False
                    Else
                        MsgBox("Unable to update Transaction")
                    End If
                Else
                    MsgBox("Please select a record to update!")
                End If

        End Select
    End Sub

    Private Function DataComplete() As Boolean
        If txtField01.Text = "" Then
            MessageBox.Show("Please enter Engine No", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField01
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField02.Text = "" Then
            MessageBox.Show("Please enter Frame No", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField02
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField04.Text = "" Then
            MessageBox.Show("Please enter Model Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField04
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField05.Text = "" Then
            MessageBox.Show("Please enter Color Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField05
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField06.Text = "" Then
            MessageBox.Show("Please enter Year Model", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField06
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField07.Text = "" Then
            MessageBox.Show("Please enter File No!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField07
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField08.Text = "" Then
            MessageBox.Show("Please enter CRNENo!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField08
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField09.Text = "" Then
            MessageBox.Show("Please Input CRNo!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField09
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField10.Text = "" Then
            MessageBox.Show("Please enter Plate No!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField10
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField11.Text = "" Then
            MessageBox.Show("Please Input Reg OR No!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField11
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField12.Text = "" Then
            MessageBox.Show("Please enter Sticker No!", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField12
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField13.Text = "" Then
            MessageBox.Show("Please enter Date of Registration", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField13
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf cmbLocation.Text = "" Then
            MessageBox.Show("Please select location", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With cmbLocation
                .Focus()
            End With
            Return False
        ElseIf txtField16.Text = "" Then
            MessageBox.Show("Please enter Name", "No entry",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField16
                .Focus()
                .SelectAll()
            End With
            Return False
        End If

        Return True
    End Function

    Private Sub p_oTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.MasterRetrieved
        Dim loTxt As TextBox
        'ind TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 13
                loTxt.Text = Format(Value, IsDate("MMMM dd, yyyy"))
            Case Else
                loTxt.Text = IFNull(Value, "")
        End Select
    End Sub

End Class
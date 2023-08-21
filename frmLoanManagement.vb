Imports MySql.Data.MySqlClient
Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmLoanManagement
    Private WithEvents p_oTrans As ggcLRTransaction.LRMasterCarNeo
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private p_AccountStatus As String

    Public Property AccountStatus() As String
        Get
            Return p_AccountStatus
        End Get
        Set(ByVal value As String)
            p_AccountStatus = value
        End Set
    End Property

    Private Sub frmloanManagement_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmloanManagement_Activated")
        'If p_AccountStatus = "10234" 
        If p_AccountStatus = "-1" Then
            Me.Text = "North Point Loan Management System"
        End If
        If pnLoadx = 1 Then
            p_oTrans.AccountStatus = p_AccountStatus
            p_oTrans.InitTransaction()
            p_oTrans.NewTransaction()
            Call newRecord()
            pnLoadx = 2
        End If
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
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0, 1
                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 0, True, False)) = True Then
                            loadMaster()
                            setTextSeeks()
                        End If

                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 2
                        Call p_oTrans.SearchMaster(2, loTxt.Text)
                        If loTxt.Text <> "" Then SetNextFocus()
                    Case 36
                        Call p_oTrans.SearchDetail(3, loTxt.Text)
                    Case 38
                        Call p_oTrans.SearchDetail(5, loTxt.Text)
                End Select
            End If
            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

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

    Private Sub frmLoanManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLoanManagement_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.LRMasterCarNeo(p_oAppDriver)
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Function isCorrectTerm(ByVal input As String) As Boolean 'Define the function and its return value.
        Try
            If (IsNumeric(input)) Then 'Checks if the input is a number
                If (input.Length <= 2) Then
                    If input = 3 Or input = 6 Or input = 12 Or input = 18 Or input = 24 Or input = 36 Or input = 48 Or input = 60 Then
                        Return True
                    End If
                End If
            End If
        Catch
            Return False
        End Try
    End Function

    Private Sub loadMaster()
        txtField00.Text = p_oTrans.Master("sAcctNmbr")
        Call loadEntry(Me.Panel1)
        Call loadEntry(Me.Panel3)
        TabControl1.SelectedIndex = 0
    End Sub

    Private Function newRecord() As Boolean
        txtField02.Focus()
        Call loadMaster()
        p_nEditMode = xeEditMode.MODE_UPDATE
        txtOther01.Text = "0.00"
        txtOther02.Text = "0.00"
        initButton()
        Return True
    End Function

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadEntry(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 1, 10, 12, 16
                            If IsDate(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                            End If
                        Case 5 To 9, 13, 14, 15, 18 To 25, 29, 30
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 31
                            loTxt.Text = IIf(p_oTrans.Master("cActivexx") = "1", "ACTIVE", "INACTIVE")
                        Case 27
                            loTxt.Text = IFNull(p_oTrans.Master("cAcctStat"), "UNKNOWN")
                            If loTxt.Text = "0" Then
                                loTxt.Text = "ACTIVE"
                            End If
                        Case 34
                            loTxt.Text = IFNull(p_oTrans.Detail("sEngineNo"), "")
                        Case 35
                            loTxt.Text = IFNull(p_oTrans.Detail("sFrameNox"), "")
                        Case 36
                            loTxt.Text = IFNull(p_oTrans.Detail("sModelNme"), "")
                        Case 37
                            loTxt.Text = IFNull(p_oTrans.Detail("sBrandNme"), "")
                        Case 38
                            loTxt.Text = IFNull(p_oTrans.Detail("sColorNme"), "")
                        Case 39
                            loTxt.Text = IFNull(p_oTrans.Detail("sFileNoxx"), "")
                        Case 40
                            loTxt.Text = IFNull(p_oTrans.Detail("sCRENoxxx"), "")
                        Case 41
                            loTxt.Text = IFNull(p_oTrans.Detail("sCRNoxxxx"), "")
                        Case 42
                            loTxt.Text = IFNull(p_oTrans.Detail("sPlateNoP"), "")
                        Case 43
                            loTxt.Text = IFNull(p_oTrans.Detail("dRegister"), Format(p_oAppDriver.getSysDate, "MMMM dd, yyyy"))
                            If Not loTxt.Text = "" Then
                                p_oTrans.Detail("dRegister") = loTxt.Text
                                loTxt.Text = Format(p_oTrans.Detail("dRegister"), "MMMM dd, yyyy")
                            End If
                        Case 44
                            loTxt.Text = IFNull(p_oTrans.Detail("nYearModl"), Format(p_oAppDriver.getSysDate, "yyyy"))
                        Case 46
                            If IFNull(p_oTrans.Detail("nSubsidze")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nSubsidze"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 48
                            If IFNull(p_oTrans.Detail("nInctvAmt")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInctvAmt"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 50
                            If IFNull(p_oTrans.Detail("nInsAmtxx")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInsAmtxx"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Master(loIndex), "")
                    End Select
            End If
            txtOther02.Text = Format(Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")), xsDECIMAL)
            txtOther01.Text = Format(Val(p_oTrans.Master("nMonAmort")) + Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")), xsDECIMAL)
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

        'mode add new/ready
        cmdButton04.Visible = Not lbShow
        cmdbutton08.Visible = Not lbShow
        cmdButton03.Visible = Not lbShow
        Panel1.Enabled = Not lbShow
        TabControl1.TabPages(0).Enabled = Not lbShow
        TabControl1.TabPages(1).Enabled = Not lbShow
        TabControl1.TabPages(2).Enabled = Not lbShow
        TabControl1.TabPages(3).Enabled = Not lbShow

        'MODE UPDATE
        cmdbutton07.Visible = lbShow
        cmdButton01.Visible = lbShow
        cmdButton05.Visible = lbShow
        cmdButton00.Visible = lbShow
        cmdButton02.Visible = lbShow
        Panel2.Enabled = lbShow
    End Sub

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
        Dim loindex As Integer
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
        loIndex = Val(Mid(loTxt.Name, 9))
        If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
            Select Case loindex
                Case 48
                    TabControl1.SelectedIndex = 1
                Case 4
                    TabControl1.SelectedIndex = 2
                Case 29
                    TabControl1.SelectedIndex = 3
            End Select
        End If
    End Sub

    ''Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then

            Select Case loIndex
                Case 1, 10, 12, 16
                    If Not IsDate(loTxt.Text) Then
                        loTxt.Text = Format(p_oAppDriver.getSysDate, "MMMM dd, yyyy")
                        p_oTrans.Master(loIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDate(loTxt.Text), "MMMM dd, yyyy")
                        p_oTrans.Master(loIndex) = loTxt.Text
                    End If
                Case 6, 8, 13, 15, 18 To 25, 29
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Master(loIndex) = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Master(loIndex) = loTxt.Text
                    End If
                Case 9
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Master("nIntRatex") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Master("nIntRatex") = loTxt.Text
                    End If
                Case 14
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Master("npenltyrt") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Master("npenltyrt") = loTxt.Text
                    End If
                Case 5
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Master("nPrincipl") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Master("nPrincipl") = loTxt.Text
                    End If
                Case 7
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Master("nSrvcChrg") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Master("nSrvcChrg") = loTxt.Text
                    End If
                Case 11
                    If isCorrectTerm(loTxt.Text) = False Then
                        MessageBox.Show("You have entered an Incorrect Term", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error)
                        loTxt.Text = "12"
                        TabControl1.SelectedIndex = 2
                        loTxt.Focus()
                        p_oTrans.Master("nAcctTerm") = loTxt.Text
                    Else
                        loTxt.Text = Format(CInt(loTxt.Text), xsINTEGER)
                        p_oTrans.Master("nAcctTerm") = loTxt.Text
                    End If
                    txtField12.Text = Format(CDate(txtField12.Text), "MMMM dd, yyy")
                    txtField06.Text = Format(CDec(txtField06.Text), xsDECIMAL)
                    txtField13.Text = Format(CDec(txtField13.Text), xsDECIMAL)
                    txtOther02.Text = Format(CDec(txtField06.Text) / CDec(txtField11.Text), xsDECIMAL)
                    txtOther01.Text = Format(CDec(txtOther02.Text) + CDec(txtField13.Text), xsDECIMAL)
                Case 34
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sEngineNo") = loTxt.Text
                Case 35
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sFrameNox") = loTxt.Text
                Case 36
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sModelNme") = loTxt.Text
                Case 37
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sBrandNme") = loTxt.Text
                Case 38
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sColorNme") = loTxt.Text
                Case 39
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sFileNoxx") = loTxt.Text
                Case 40
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sCRENoxxx") = loTxt.Text
                Case 41
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sCRNoxxxx") = loTxt.Text
                Case 42
                    loTxt.Text = UCase(loTxt.Text)
                    p_oTrans.Detail("sPlateNoP") = loTxt.Text
                Case 43
                    If Not IsDate(loTxt.Text) Then
                        loTxt.Text = Format(p_oAppDriver.getSysDate, "MMMM dd, yyyy")
                        p_oTrans.Detail("dRegister") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDate(loTxt.Text), "MMMM dd, yyyy")
                        p_oTrans.Detail("dRegister") = loTxt.Text
                    End If
                Case 44
                    If isKnownGoodDate(loTxt.Text) = False Then
                        MessageBox.Show("Incorrect Year Model!", "Error!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error)
                        loTxt.Text = Format(p_oAppDriver.getSysDate, "yyyy")
                        loTxt.Focus()
                        p_oTrans.Detail("nYearModl") = loTxt.Text
                    Else
                        loTxt.Text = Format(CInt(loTxt.Text))
                        p_oTrans.Detail("nYearModl") = loTxt.Text
                    End If
                Case 46
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Detail("nSubsidze") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Detail("nSubsidze") = loTxt.Text
                    End If
                Case 48
                    If Not IsNumeric(loTxt.Text) Then
                        loTxt.Text = "0.00"
                        p_oTrans.Detail("nInctvAmt") = loTxt.Text
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        p_oTrans.Detail("nInctvAmt") = loTxt.Text
                    End If
                Case Else
                    p_oTrans.Master(loIndex) = loTxt.Text
            End Select
        End If
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

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 1 ' Update
                If Not p_oTrans.Master("sAcctNmbr") = "" And Not p_oTrans.Master("sClientNm") = "" Then
                    If txtField27.Text = "UNKNOWN" Then
                        p_nEditMode = xeEditMode.MODE_READY
                        initButton()
                        txtField02.Focus()
                    Else
                        MessageBox.Show("Unable to update transaction" + Environment.NewLine + "Already posted!", "Error!",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Please select a record to update!", "No Record",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Case 2 ' Browse
                If p_oTrans.SearchTransaction("", False) = True Then
                    loadMaster()
                    p_nEditMode = xeEditMode.MODE_READY
                    setTextSeeks()
                End If
        
            Case 3 ' Cancel
                If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "NorthPoint Entry") = MsgBoxResult.Yes Then
                    p_oTrans.InitTransaction()
                    p_oTrans.NewTransaction()
                    Call newRecord()
                End If

            Case 4 ' Save
                If entryOK() Then
                    If p_oTrans.SaveTransaction Then
                        MsgBox("Transaction was save successfully!", MsgBoxStyle.Information, "Car Trade Loan Entry")
                        If MsgBox("Do you want to POST the transaction", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Car Trade Entry Confirmation") = MsgBoxResult.Ok Then
                            If Not p_oTrans.PostTransaction Then
                                MsgBox("Unable to POST Loan Entry!", MsgBoxStyle.Information, "Car Trade Loan Entry")
                            End If
                            Call p_oTrans.NewTransaction()
                            loadMaster()
                            ClearTextBoxes()
                            p_nEditMode = xeEditMode.MODE_READY
                            initButton()
                        End If
                        p_nEditMode = xeEditMode.MODE_UPDATE
                        initButton()
                    Else
                        MsgBox("Please check your Entry!", MsgBoxStyle.Information, "Car Trade Loan Entry")
                        p_nEditMode = xeEditMode.MODE_UPDATE
                    End If
                End If
            Case 5 ' Ledger
                Dim loFrm As New frmAutoLedger
                loFrm.AccountNo = p_oTrans.Master("sAcctNmbr")
                loFrm.ClientName = p_oTrans.Master("sClientNm")
                loFrm.Address = IFNull(p_oTrans.Master("xAddressx"), "")
                loFrm.CarModel = IFNull(p_oTrans.Detail("sModelNme"), "")
                loFrm.PlateNo = IFNull(p_oTrans.Detail("sPlateNoP"), "")
                loFrm.ShowDialog()
            Case 7 ' New
                If p_oTrans.NewTransaction Then
                    Call loadEntry(Me.Panel1)
                    Call loadEntry(Me.Panel3)
                    TabControl1.SelectedIndex = 0
                    p_nEditMode = xeEditMode.MODE_READY
                    initButton()
                    ClearTextBoxes()
                    txtField02.Focus()
                End If

            Case 8 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 9))
                If loIndex = 36 Then
                    p_oTrans.SearchDetail(3, poControl.Text)
                End If
                If loIndex = 38 Then
                    p_oTrans.SearchDetail(5, poControl.Text)
                End If
                If loIndex = 2 Then
                    p_oTrans.SearchMaster(2, poControl.Text)
                Else
                    If txtField02.Text <> "" Then SetNextFocus()
                End If
        End Select
    End Sub

    Private Function entryOK() As Boolean
        If txtField02.Text = "" Then
            MessageBox.Show("Please enter Client Name", "No Record",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField02
                .Focus()
                .SelectAll()
            End With
            Return False
        ElseIf txtField34.Text = "" Then
            MessageBox.Show("Please enter Engine No!", "No Record",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField34
                .Focus()
                .SelectAll()
                TabControl1.SelectedIndex = 3
            End With
            Return False
        ElseIf txtField35.Text = "" Then
            MessageBox.Show("Please enter Frame No!", "No Record",
                             MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField35
                .Focus()
                .SelectAll()
                TabControl1.SelectedIndex = 3
            End With
            Return False
        ElseIf txtField11.Text = "0" Then
            MessageBox.Show("Please enter correct Term!", "No Record",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            With txtField11
                TabControl1.SelectedIndex = 2
                .Focus()
                .SelectAll()

            End With
            Return False
        End If
        Return True
    End Function

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window

    End Sub

    Private Function setTextSeeks() As Boolean
        txtSeeks00.Text = p_oTrans.Master("sAcctNmbr")
        txtSeeks01.Text = txtField02.Text
        Return True

    End Function

    Public Sub ClearTextBoxes()
        txtSeeks00.Text = ""
        txtSeeks01.Text = ""
        txtField03.Text = ""
        txtField34.Text = ""
        txtField34.Text = ""
        txtField35.Text = ""
        txtField37.Text = ""
        txtField36.Text = ""
        txtField38.Text = ""
        txtField39.Text = ""
        txtField40.Text = ""
        txtField41.Text = ""
        txtField42.Text = ""
        txtOther01.Text = "0.00"
        txtOther02.Text = "0.00"
        txtField43.Text = Format(p_oAppDriver.getSysDate(), "MMMM dd, yyyy")
        txtField44.Text = Format(p_oAppDriver.getSysDate(), "yyyy")
        txtField27.Text = "UNKNOWN"
    End Sub

    Private Sub p_oTrans_DetailRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.DetailRetrieved
        Select Case Index
            Case 3
                txtField36.Text = IFNull(Value, "")
            Case 4
                txtField37.Text = IFNull(Value, "")
            Case 5
                txtField38.Text = IFNull(Value, "")
        End Select
    End Sub

    Private Sub poTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.MasterRetrieved
        Dim loTxt As TextBox
        'ind TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)
        Select Case Index
            Case 43
                loTxt.Text = Format(CDate(Value), "MMMM dd, yyyy")
            Case Else
                loTxt.Text = IFNull(Value, "")
        End Select
    End Sub
End Class
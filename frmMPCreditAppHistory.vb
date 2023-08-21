Imports ggcAppDriver
Imports ggcGOCAS

Public Class frmMPCreditAppHistory
    Dim lnMsg As String
    Dim poControl As Control
    Dim pnLoadx As Integer
    Private pxeModuleName As String = "E-Commerce MP Credit Application History"
    Private pnReference As Integer = 0
    Private pnEmail As Integer = 0
    Private pnSEmail As Integer = 0
    Private pnMobile As Integer = 0
    Private pnSMobile As Integer = 0
    Private pnComakMobile As Integer = 0
    Private WithEvents poTrans As MPApplication

    Private Sub frmMPCreditAppHistory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            poTrans = New MPApplication(p_oAppDriver, -1)
            Call initTransaction()
            pnLoadx = 2
        End If
    End Sub

    Private Sub initTransaction()
        'clear all text fields and lables
        Call clearFields(Me.Panel1)
        Call clearFields(Me.Panel2)

        Call initClientReference(dgvDetail)
        Call initNumber(dgvDetail03)
        Call initEmail(dgvDetail04)
        Call initSMobile(dgvDetail05)
        Call initEmail(dgvDetail06)
        Call initSMobile(dgvDetail07)

        'initialize listview

        txtField90.Focus()
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0
                Me.Close()
            Case 3
                If poTrans.SearchTransaction("", True) = True Then
                    Call ClearFields(Me.Panel1)
                    Call ClearFields(Me.Panel2)
                    Call loadTransaction()
                    txtField90.Text = poTrans.Master("sTransNox")
                    txtField91.Text = poTrans.Master("sClientNm")
                Else
                    Call ClearFields(Me.Panel1)
                    Call ClearFields(Me.Panel2)
                End If
        End Select
    End Sub

    Private Sub loadAppliMobile()
        With dgvDetail03
            .Rows.Clear()
            If poTrans.Category.applicant_info.mobile_number.Count = 0 Then
                poTrans.Category.applicant_info.mobile_number.Add(New ggcGOCAS.GOCASConst.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.applicant_info.mobile_number.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.applicant_info.mobile_number(lnCtr).sMobileNo
                If poTrans.Category.applicant_info.mobile_number(lnCtr).cPostPaid = "" Then poTrans.Category.applicant_info.mobile_number(lnCtr).cPostPaid = 0
                .Rows(lnCtr).Cells(2).Value = IIf(poTrans.Category.applicant_info.mobile_number(lnCtr).cPostPaid = 0, "Prepaid", "PostPaid")
                lnCtr = lnCtr + 1
            Loop
        End With

        dgvDetail03.CurrentCell = dgvDetail03.Rows(dgvDetail03.RowCount - 1).Cells(0)
        dgvDetail03.Rows(dgvDetail03.RowCount - 1).Selected = True
    End Sub

    Private Sub loadSpouseMobile()
        With dgvDetail05
            .Rows.Clear()
            If poTrans.Category.spouse_info.personal_info.mobile_number.Count = 0 Then
                poTrans.Category.spouse_info.personal_info.mobile_number.Add(New ggcGOCAS.GOCASConst.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.spouse_info.personal_info.mobile_number.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.spouse_info.personal_info.mobile_number(lnCtr).sMobileNo
                lnCtr = lnCtr + 1
            Loop
        End With

        dgvDetail05.CurrentCell = dgvDetail05.Rows(dgvDetail05.RowCount - 1).Cells(0)
        dgvDetail05.Rows(dgvDetail05.RowCount - 1).Selected = True
    End Sub

    Private Sub loadComakMobile()
        With dgvDetail07
            .Rows.Clear()
            If poTrans.Category.comaker_info.mobile_number.Count = 0 Then
                poTrans.Category.comaker_info.mobile_number.Add(New ggcGOCAS.GOCASConst.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.comaker_info.mobile_number.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.comaker_info.mobile_number(lnCtr).sMobileNo
                lnCtr = lnCtr + 1
            Loop
        End With

        dgvDetail07.CurrentCell = dgvDetail07.Rows(dgvDetail07.RowCount - 1).Cells(0)
        dgvDetail07.Rows(dgvDetail07.RowCount - 1).Selected = True
    End Sub

    Private Sub loadAppliEmail()
        With dgvDetail04
            .Rows.Clear()
            If poTrans.Category.applicant_info.email_address.Count = 0 Then
                poTrans.Category.applicant_info.email_address.Add(New ggcGOCAS.GOCASConst.email_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.applicant_info.email_address.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.applicant_info.email_address(lnCtr).sEmailAdd
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail04.CurrentCell = dgvDetail04.Rows(dgvDetail04.RowCount - 1).Cells(0)
        dgvDetail04.Rows(dgvDetail04.RowCount - 1).Selected = True
    End Sub

    Private Sub loadSpouseEmail()
        With dgvDetail06
            .Rows.Clear()
            If poTrans.Category.spouse_info.personal_info.email_address.Count = 0 Then
                poTrans.Category.spouse_info.personal_info.email_address.Add(New ggcGOCAS.GOCASConst.email_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.spouse_info.personal_info.email_address.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.spouse_info.personal_info.email_address(lnCtr).sEmailAdd
                lnCtr = lnCtr + 1
            Loop
        End With

        dgvDetail05.CurrentCell = dgvDetail05.Rows(dgvDetail05.RowCount - 1).Cells(0)
        dgvDetail05.Rows(dgvDetail05.RowCount - 1).Selected = True
    End Sub

    Private Sub frmCreditApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtAppli", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtApRes", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtRefer", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoIn", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtResid", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoEm", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtComak", "GotFocus", AddressOf txtField_GotFocus)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtAppli", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtApRes", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtRefer", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoIn", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtResid", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoEm", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtComak", "LostFocus", AddressOf txtField_LostFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtAppli", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtApRes", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtRefer", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoIn", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtResid", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoEm", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtOther", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtComak", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
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

            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 90, 91
                        If poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 90, True, False)) Then
                            Call ClearFields(Me.Panel1)
                            Call ClearFields(Me.Panel2)
                            Call loadTransaction()
                            txtField90.Text = poTrans.Master("sTransNox")
                            txtField91.Text = poTrans.Master("sClientNm")
                        Else
                            Call ClearFields(Me.Panel1)
                            Call ClearFields(Me.Panel2)
                        End If
                End Select
            End If
            '###########################
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub ClearFields(ByVal loControl As Control)
        Dim loTxt As Control
        Dim loIndex As Integer
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call ClearFields(loTxt)
            Else
                If (TypeOf loTxt Is Label) Then
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "lblAppli" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblApRes" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblEmplo" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblSpoIn" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblSpoRe" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblSpoEm" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblOther" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblComak" Then
                        Select Case loIndex
                            Case Else
                                loTxt.Text = "N/A"
                        End Select
                    End If
                Else
                    If (TypeOf loTxt Is TextBox) Then
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtAppli" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtApRes" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtRefer" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoIn" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtResid" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtOther" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtComak" Then
                            Select Case loIndex
                                Case Else
                                    loTxt.Text = ""
                            End Select
                        End If
                    Else
                        If (TypeOf loTxt Is ComboBox) Then
                            DirectCast(loTxt, ComboBox).SelectedIndex = -1
                        End If
                    End If
                End If
            End If
        Next
        pnReference = 0
        pnEmail = 0
        pnSEmail = 0
        pnMobile = 0
        pnSMobile = 0
        pnComakMobile = 0
        setTranStat(-1, lblStatus)
        tabControl00.SelectedIndex = 0
    End Sub

    Private Sub loadMainInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMainInfo(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    With poTrans
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .Master("sTransNox")
                            Case 1
                                loTxt.Text = poTrans.getBranch(poTrans.Master("sBranchCd"), False, True, "")
                            Case 2
                                loTxt.Text = .Master("sClientNm")
                            Case 81
                                loTxt.Text = .Detail.sPresAddr
                            Case 3
                                If Not IsDate(.Master("dTransact")) Then .Master("dTransact") = p_oAppDriver.getSysDate
                                loTxt.Text = Format(.Master("dTransact"), xsDATE_MEDIUM)
                            Case 4
                                If Not IsDate(.Detail.dBirthDte) Then .Detail.dBirthDte = p_oAppDriver.getSysDate
                                loTxt.Text = Format(CDate(.Detail.dBirthDte), xsDATE_MEDIUM)
                            Case 5
                                If Not IsNumeric(.Detail.nAgexxxxx) Then .Detail.nAgexxxxx = 0
                                loTxt.Text = CInt(.Detail.nAgexxxxx)
                            Case 6
                                loTxt.Text = poTrans.getModel(.Category.sModelIDx, False, True, "")
                            Case 7
                                If Not IsNumeric(.Category.nAcctTerm) Then .Category.nAcctTerm = 0
                                loTxt.Text = CInt(.Category.nAcctTerm)
                            Case 8
                                If Not IsNumeric(.Category.nDownPaym) Then .Category.nDownPaym = 0
                                loTxt.Text = FormatNumber(.Category.nDownPaym, 2)
                        End Select
                        If (.Category.cUnitAppl <> "") Then setApplicationType(.Category.cUnitAppl, cmb27)
                        If (.Category.cApplType <> "") Then setTypeOfCustomer(.Category.cApplType, cmb28)
                        setTranStat(.Master("cTranStat"), lblStatus)
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub loadApplicantInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadApplicantInfo(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtAppli" Then
                        With poTrans.Category.applicant_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sFrstName
                                Case 1
                                    loTxt.Text = .sMiddName
                                Case 2
                                    loTxt.Text = .sLastName
                                Case 3
                                    loTxt.Text = .sSuffixNm
                                Case 4
                                    loTxt.Text = .sNickName
                                Case 7
                                    If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                    loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                Case 8
                                    loTxt.Text = poTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 10
                                    loTxt.Text = .sMaidenNm
                                Case 13
                                    loTxt.Text = .facebook.sFBAcctxx
                                Case 14
                                    loTxt.Text = .sVibeAcct
                                Case 15
                                    loTxt.Text = poTrans.getCountry(.sCitizenx, False, True, "")
                            End Select
                            If (.cGenderCd <> "") Then setGender(.cGenderCd, cmb00)
                            If (.cCvilStat <> "") Then setCivilStat(.cCvilStat, cmb01)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadAppEmployment(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadAppEmployment(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Then
                        With poTrans.Category.means_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .employed.sEmployer
                                Case 1
                                    loTxt.Text = poTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                Case 2
                                    loTxt.Text = .employed.sWrkTelno
                                Case 3
                                    If Not IsNumeric(.employed.nLenServc) Then .employed.nLenServc = 0
                                    loTxt.Text = CInt(.employed.nLenServc)
                                Case 4
                                    If Not IsNumeric(.employed.nSalaryxx) Then .employed.nSalaryxx = 0
                                    loTxt.Text = FormatNumber(.employed.nSalaryxx, 2)
                                Case 5
                                    loTxt.Text = poTrans.getOccupation(.employed.sPosition, False, True, "")
                                Case 6
                                    loTxt.Text = .self_employed.sIndstBus
                                Case 7
                                    loTxt.Text = poTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                Case 9
                                    If Not IsNumeric(.self_employed.nBusIncom) Then .self_employed.nBusIncom = 0
                                    loTxt.Text = FormatNumber(.self_employed.nBusIncom, 2)
                                Case 10
                                    If Not IsNumeric(.self_employed.nBusLenxx) Then .self_employed.nBusLenxx = CInt(0)
                                    loTxt.Text = CInt(.self_employed.nBusLenxx)
                                Case 11
                                    loTxt.Text = .other_income.sOthrIncm
                                Case 12
                                    loTxt.Text = poTrans.getCountry(.employed.sOFWNatnx, False, True, "")
                            End Select
                            If (.employed.cEmpStatx <> "") Then setStatEmployment(.employed.cEmpStatx, cmb11)

                            If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb17)
                            If (.employed.cEmpSectr <> "") Then setEmploymentSector(.employed.cEmpSectr, cmb18)
                            If (.employed.cUniforme <> "") Then setIsUniformed(.employed.cUniforme, cmb19)
                            If (.employed.cMilitary <> "") Then setIsMilitaryUniformed(.employed.cMilitary, cmb20)
                            If (.employed.cGovtLevl <> "") Then setGovernmentLevel(.employed.cGovtLevl, cmb21)
                            If (.employed.cCompLevl <> "") Then setCompanyLevel(.employed.cCompLevl, cmb22)
                            If (.employed.cEmpLevlx <> "") Then setEmploymentLevel(.employed.cEmpLevlx, cmb23)
                            If (.employed.cOcCatgry <> "") Then setOccptCateg(.employed.cOcCatgry, cmb24)
                            If (.employed.cOFWRegnx <> "") Then setOFReg(.employed.cOFWRegnx, cmb25)
                        End With
                    End If
                End If
            End If
        Next

    End Sub

    Private Sub loadApRes(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadApRes(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtApRes" Then
                        With poTrans.Category.residence_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .present_address.sLandMark
                                Case 1
                                    loTxt.Text = .present_address.sHouseNox
                                Case 2
                                    loTxt.Text = .present_address.sAddress1
                                Case 3
                                    loTxt.Text = .present_address.sAddress2
                                Case 4
                                    loTxt.Text = poTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                Case 5
                                    loTxt.Text = poTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                Case 6
                                    If Not IsNumeric(.rent_others.nLenStayx) Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = CInt(.rent_others.nLenStayx)
                                    End If
                                Case 7
                                    If Not IsNumeric(.rent_others.nRentExps) Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = CDbl(.rent_others.nRentExps)
                                    End If
                                Case 8
                                    loTxt.Text = .sCtkReltn
                                Case 9
                                    loTxt.Text = .permanent_address.sLandMark
                                Case 10
                                    loTxt.Text = .permanent_address.sHouseNox
                                Case 11
                                    loTxt.Text = .permanent_address.sAddress1
                                Case 12
                                    loTxt.Text = .permanent_address.sAddress2
                                Case 13
                                    loTxt.Text = poTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                Case 14
                                    loTxt.Text = poTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                            End Select
                            If (.cOwnershp <> "") Then setOwnership(.cOwnershp, cmb02)
                            If (.cOwnOther <> "") Then setOwnedOther(.cOwnOther, cmb03)
                            If (.rent_others.cRntOther <> "") Then setRent(.rent_others.cRntOther, cmb04)
                            If (.cHouseTyp <> "") Then setHouseType(.cHouseTyp, cmb05)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadSpouseRes(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseRes(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtResid" Then
                        With poTrans.Category.spouse_info.residence_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .present_address.sLandMark
                                Case 1
                                    loTxt.Text = .present_address.sHouseNox
                                Case 2
                                    loTxt.Text = .present_address.sAddress1
                                Case 3
                                    loTxt.Text = .present_address.sAddress2
                                Case 4
                                    loTxt.Text = poTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                Case 5
                                    loTxt.Text = poTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                Case 6
                                    If Not IsNumeric(.rent_others.nLenStayx) Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = CInt(.rent_others.nLenStayx)
                                    End If
                                Case 7
                                    If Not IsNumeric(.rent_others.nRentExps) Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = CDbl(.rent_others.nRentExps)
                                    End If
                                Case 8
                                    loTxt.Text = .sCtkReltn
                                Case 9
                                    loTxt.Text = .permanent_address.sLandMark
                                Case 10
                                    loTxt.Text = .permanent_address.sHouseNox
                                Case 11
                                    loTxt.Text = .permanent_address.sAddress1
                                Case 12
                                    loTxt.Text = .permanent_address.sAddress2
                                Case 13
                                    loTxt.Text = poTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                Case 14
                                    loTxt.Text = poTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                            End Select
                            If (.cOwnershp <> "") Then setOwnership(.cOwnershp, cmb07)
                            If (.cOwnOther <> "") Then setOwnedOther(.cOwnOther, cmb08)
                            If (.rent_others.cRntOther <> "") Then setRent(.rent_others.cRntOther, cmb09)
                            If (.cHouseTyp <> "") Then setHouseType(.cHouseTyp, cmb10)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadSpouseInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseInfo(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoIn" Then
                        With poTrans.Category.spouse_info.personal_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sFrstName
                                Case 1
                                    loTxt.Text = .sMiddName
                                Case 2
                                    loTxt.Text = .sLastName
                                Case 3
                                    loTxt.Text = .sSuffixNm
                                Case 4
                                    loTxt.Text = .sNickName
                                Case 7
                                    If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                    loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                Case 8
                                    loTxt.Text = poTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 9
                                    loTxt.Text = poTrans.getCountry(.sCitizenx, False, True, "")
                            End Select
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadComaker(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadComaker(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtComak" Then
                        With poTrans.Category.comaker_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sFrstName
                                Case 1
                                    loTxt.Text = .sMiddName
                                Case 2
                                    loTxt.Text = .sLastName
                                Case 3
                                    loTxt.Text = poTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 7
                                    If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                    loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                            End Select
                            If (.sReltnCde <> "") Then setFinanceType(.sReltnCde, cmb13)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadOther(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadOther(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtOther" Then
                        With poTrans.Category.disbursement_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .bank_account.sBankName
                                Case 1
                                    If Not IsNumeric(.monthly_expenses.nElctrcBl) Then .monthly_expenses.nElctrcBl = 0
                                    loTxt.Text = FormatNumber(CDbl(.monthly_expenses.nElctrcBl), 2)
                                Case 2
                                    If Not IsNumeric(.monthly_expenses.nWaterBil) Then .monthly_expenses.nWaterBil = 0
                                    loTxt.Text = FormatNumber(CDbl(.monthly_expenses.nWaterBil), 2)
                                Case 3
                                    If Not IsNumeric(.monthly_expenses.nLoanAmtx) Then .monthly_expenses.nLoanAmtx = 0
                                    loTxt.Text = FormatNumber(CDbl(.monthly_expenses.nLoanAmtx), 2)
                                Case 4
                                    loTxt.Text = .credit_card.sBankName
                                Case 5
                                    If Not IsNumeric(.credit_card.nCrdLimit) Then .credit_card.nCrdLimit = 0
                                    loTxt.Text = FormatNumber(CDbl(.credit_card.nCrdLimit), 2)
                            End Select
                            If (.bank_account.sAcctType <> "") Then setBankType(.bank_account.sAcctType, cmb26)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadSpouseEmployment(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseEmployment(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Then
                        With poTrans.Category.spouse_means
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .employed.sEmployer
                                Case 1
                                    loTxt.Text = poTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                Case 2
                                    loTxt.Text = .employed.sWrkTelno
                                Case 3
                                    If Not IsNumeric(.employed.nLenServc) Then .employed.nLenServc = 0
                                    loTxt.Text = .employed.nLenServc
                                Case 4
                                    If Not IsNumeric(.employed.nSalaryxx) Then .employed.nSalaryxx = 0
                                    loTxt.Text = FormatNumber(CDbl(.employed.nSalaryxx), 2)
                                Case 5
                                    loTxt.Text = poTrans.getOccupation(.employed.sPosition, False, True, "")
                                Case 6
                                    loTxt.Text = .self_employed.sIndstBus
                                Case 7
                                    loTxt.Text = poTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                Case 9
                                    If Not IsNumeric(.self_employed.nBusIncom) Then .self_employed.nBusIncom = 0
                                    loTxt.Text = FormatNumber(CDbl(.self_employed.nBusIncom), 2)
                                Case 10
                                    If Not IsNumeric(.self_employed.nBusLenxx) Then .self_employed.nBusLenxx = 0
                                    loTxt.Text = CInt(.self_employed.nBusLenxx)
                                Case 11
                                    loTxt.Text = .other_income.sOthrIncm
                                Case 12
                                    loTxt.Text = poTrans.getCountry(.employed.sOFWNatnx, False, True, "")
                            End Select
                            If (.employed.cEmpStatx <> "") Then setStatEmployment(.employed.cEmpStatx, cmb14)

                            If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb35)
                            If (.employed.cEmpSectr <> "") Then setEmploymentSector(.employed.cEmpSectr, cmb36)
                            If (.employed.cUniforme <> "") Then setIsUniformed(.employed.cUniforme, cmb37)
                            If (.employed.cMilitary <> "") Then setIsMilitaryUniformed(.employed.cMilitary, cmb38)
                            If (.employed.cGovtLevl <> "") Then setGovernmentLevel(.employed.cGovtLevl, cmb39)
                            If (.employed.cCompLevl <> "") Then setCompanyLevel(.employed.cCompLevl, cmb40)
                            If (.employed.cEmpLevlx <> "") Then setEmploymentLevel(.employed.cEmpLevlx, cmb41)
                            If (.employed.cOcCatgry <> "") Then setOccptCateg(.employed.cOcCatgry, cmb42)
                            If (.employed.cOFWRegnx <> "") Then setOFReg(.employed.cOFWRegnx, cmb43)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadTransaction()
        Call loadMainInfo(Me.Panel1)
        Call loadApplicantInfo(Me.tabPage00)
        Call loadApRes(Me.tabPage08)
        Call loadAppEmployment(Me.tabPage01)
        Call loadSpouseInfo(Me.tabPage03)
        Call loadSpouseRes(Me.tabPage07)
        Call loadSpouseEmployment(Me.tabPage04)
        Call loadOther(Me.tabPage05)
        Call loadComaker(Me.tabPage06)
        Call loadReferenceCategory()
        Call loadAppliMobile()
        Call loadAppliEmail()
        Call loadSpouseMobile()
        Call loadSpouseEmail()
        Call loadComakMobile()
    End Sub

    Private Sub loadReferenceCategory()
        With dgvDetail
            .Rows.Clear()
            If poTrans.Category.other_info.personal_reference.Count = 0 Then
                poTrans.Category.other_info.personal_reference.Add(New ggcGOCAS.GOCASConst.personal_reference_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.other_info.personal_reference.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.other_info.personal_reference(lnCtr).sRefrNmex
                .Rows(lnCtr).Cells(2).Value = poTrans.getTownCity(poTrans.Category.other_info.personal_reference(lnCtr).sRefrTown, False, True, "")
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail.CurrentCell = dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(0)
        dgvDetail.Rows(dgvDetail.RowCount - 1).Selected = True
    End Sub

    Private Sub dgvDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail.Click
        Dim pdRow As Integer
        pdRow = dgvDetail.CurrentRow.Index
        ShowReference(pdRow)
    End Sub

    Private Sub ShowReference(ByVal fnRow As Integer)
        With poTrans.Category
            If .other_info.personal_reference.Count = 0 Then Exit Sub
            txtRefer00.Text = .other_info.personal_reference(fnRow).sRefrNmex
            txtRefer01.Text = poTrans.getTownCity(.other_info.personal_reference(fnRow).sRefrTown, False, True, "")
            txtRefer02.Text = .other_info.personal_reference(fnRow).sRefrMPNx
        End With
        pnReference = fnRow
    End Sub

    Private Sub dgvDetail03_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail03.Click
        Dim pdRow As Integer
        pdRow = dgvDetail03.CurrentRow.Index
        ShowMobile(pdRow)
    End Sub

    Private Sub dgvDetail04_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail04.Click
        Dim pdRow As Integer
        pdRow = dgvDetail04.CurrentRow.Index
        Call showEmail(pdRow)
    End Sub

    Private Sub dgvDetail05_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail05.Click
        Dim pdRow As Integer
        pdRow = dgvDetail05.CurrentRow.Index
        Call ShowSpouseMobile(pdRow)
    End Sub

    Private Sub dgvDetail06_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail06.Click
        Dim pdRow As Integer
        pdRow = dgvDetail06.CurrentRow.Index
        Call showSpouseEmail(pdRow)
    End Sub

    Private Sub dgvDetail07_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail07.Click
        Dim pnRow As Integer
        pnRow = dgvDetail07.CurrentRow.Index
        Call ShowComakMobile(pnRow)
    End Sub

    Public Sub showEmail(ByVal fnRow As Integer)
        If poTrans.Category.applicant_info.email_address.Count = 0 Then Exit Sub
        With poTrans.Category.applicant_info
            txtAppli06.Text = .email_address(fnRow).sEmailAdd
        End With
        pnEmail = fnRow
    End Sub

    Public Sub showSpouseEmail(ByVal fnRow As Integer)
        If poTrans.Category.spouse_info.personal_info.email_address.Count = 0 Then Exit Sub
        With poTrans.Category.spouse_info.personal_info
            txtSpoIn06.Text = .email_address(fnRow).sEmailAdd
        End With
        pnSEmail = fnRow
    End Sub

    Private Sub ShowMobile(ByVal fnRow)
        With poTrans.Category.applicant_info
            If .mobile_number.Count = 0 Then Exit Sub
            txtAppli05.Text = .mobile_number(fnRow).sMobileNo
            If IsNothing(.mobile_number(fnRow).cPostPaid) Then .mobile_number(fnRow).cPostPaid = ""
            If .mobile_number(fnRow).cPostPaid <> "1" Then
                chk02.Checked = False
            Else
                chk02.Checked = True
            End If
        End With
        pnMobile = fnRow
    End Sub

    Private Sub ShowSpouseMobile(ByVal fnRow)
        With poTrans.Category.spouse_info.personal_info
            If .mobile_number.Count = 0 Then Exit Sub
            txtSpoIn05.Text = .mobile_number(fnRow).sMobileNo
        End With
        pnSMobile = fnRow
    End Sub

    Private Sub ShowComakMobile(ByVal fnRow)
        With poTrans.Category.comaker_info
            If .mobile_number.Count = 0 Then Exit Sub
            txtComak05.Text = .mobile_number(fnRow).sMobileNo
        End With
        pnComakMobile = fnRow
    End Sub

End Class
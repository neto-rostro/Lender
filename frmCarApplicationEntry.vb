Imports ggcLRTransaction
Imports ggcAppDriver
Imports ggcGOCAS

Public Class frmCarApplicationEntry
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private WithEvents p_oTrans As ggcGOCAS.CARApplication
    Private pnMobile As Integer = 0
    Private pnEmail As Integer = 0
    Private pnLandline As Integer = 0
    Private pnChild As Integer = 0
    Private pnReference As Integer = 0
    Private pnSMobile As Integer = 0
    Private pnSLandLine As Integer = 0
    Private pnSEmail As Integer = 0
    Private pnComak As Integer = 0

    Private Sub frmCarApplicationEntry_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            initChild(dgv00)
            initRefer(dgv01)
            initSpouseMobile(dgv02)
            initLandline(dgv03)
            initEmail(dgv04)
            initSpouseMobile(dgv05)
            initLandline(dgv06)
            initEmail(dgv07)
            initSpouseMobile(dgv08)
            Call p_oTrans.NewTransaction()
            ClearFields(Me.Panel1)
            ClearFields(Me.Panel2)
            initButton(0)
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmCarApplicationEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            p_oTrans = New ggcGOCAS.CARApplication(p_oAppDriver, 0)

            'Set event Handlers for txtField
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtIntro", "Validating", AddressOf txtIntro_Validating)            
            Call grpCancelHandler(Me, GetType(TextBox), "txtCoMak", "Validating", AddressOf txtCoMak_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtPerso", "Validating", AddressOf txtPerso_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtResid", "Validating", AddressOf txtResid_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtEmplo", "Validating", AddressOf txtEmplo_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtDisbu", "Validating", AddressOf txtDisbu_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSpoIn", "Validating", AddressOf txtSpoIn_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSpoEm", "Validating", AddressOf txtSpoEm_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtOther", "Validating", AddressOf txtOther_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSpoRe", "Validating", AddressOf txtSpoRe_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtOther", "Validating", AddressOf txtOther_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtIntro", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtCoMak", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtResid", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDisbu", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoIn", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoEm", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoRe", "GotFocus", AddressOf txtField_GotFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtIntro", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtCoMak", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtResid", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDisbu", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoIn", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoEm", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtOther", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoRe", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtIntro", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtCoMak", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtResid", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDisbu", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoIn", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoEm", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpoRe", "LostFocus", AddressOf txtField_LostFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtIntro", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtCoMak", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtResid", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDisbu", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoIn", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoEm", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtOther", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoRe", "KeyDown", AddressOf ArrowKeys_Keydown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            Call grpKeyHandler(Me, GetType(ComboBox), "cmb", "KeyDown", AddressOf cmb_KeyDown)
            Call grpEventHandler(Me, GetType(ComboBox), "cmb", "SelectedIndexChanged", AddressOf combobox_SelectedIndexChanged)

            pnLoadx = 1
        End If
    End Sub

    Private Sub txtIntro_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        With p_oTrans.Detail
            If Mid(loTxt.Name, 1, 8) = "txtIntro" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 1
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nDownPaym = CDbl(loTxt.Text)
                    Case 2
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nAcctTerm = CDbl(loTxt.Text)
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nMonAmort = CDbl(loTxt.Text)
                    Case 4
                        .sUnitAppl = loTxt.Text
                    Case 6
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dTargetDt = CDate(loTxt.Text)
                    Case 7
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .nRebatesx = CDbl(loTxt.Text)
                    Case 8
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .nPNValuex = CDbl(loTxt.Text)
                End Select
            End If
        End With
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 2
                    If Not IsDate(loTxt.Text) Then loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                    p_oTrans.Master("dTransact") = CDate(loTxt.Text)
            End Select
            End If
    End Sub


    Private Sub txtSpoIn_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.spouse_info.personal_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtSpoIn" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .sLastName = loTxt.Text
                    Case 1
                        .sFrstName = loTxt.Text
                    Case 2
                        .sSuffixNm = loTxt.Text
                    Case 3
                        .sMiddName = loTxt.Text
                    Case 4
                        .sNickName = loTxt.Text
                    Case 5
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dBirthDte = CDate(loTxt.Text)
                    Case 8
                        .mobile_number(pnSMobile).sMobileNo = loTxt.Text
                        Call loadSpouseMobile()
                    Case 11
                        .landline(pnSLandLine).sPhoneNox = loTxt.Text
                        Call loadSpouseLandline()
                    Case 14
                        .sMaidenNm = loTxt.Text
                    Case 15
                        .email_address(pnSEmail).sEmailAdd = loTxt.Text
                        Call loadSpouseEmail()
                    Case 18
                        .facebook.sFBAcctxx = loTxt.Text
                    Case 21
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .facebook.nNoFriend = CInt(loTxt.Text)
                    Case 22
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .facebook.nYearxxxx = CInt(loTxt.Text)
                    Case 19
                        .sVibeAcct = loTxt.Text
                End Select
            End If
        End With
    End Sub

    Private Sub txtSpoEm_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.spouse_means
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtSpoEm" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .employed.sIndstWrk = loTxt.Text
                    Case 1
                        .employed.sEmployer = loTxt.Text
                    Case 2
                        .employed.sWrkAddrx = loTxt.Text
                    Case 5
                        .employed.sFunction = loTxt.Text
                    Case 7
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .employed.nLenServc = CInt(loTxt.Text)
                    Case 8
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(CDbl(0), xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .employed.nSalaryxx = CDbl(loTxt.Text)
                    Case 9
                        .employed.sWrkTelno = loTxt.Text
                    Case 10
                        .self_employed.sIndstBus = loTxt.Text
                    Case 11
                        .self_employed.sBusiness = loTxt.Text
                    Case 12
                        .self_employed.sBusAddrx = loTxt.Text
                    Case 15
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .self_employed.nBusLenxx = CInt(loTxt.Text)
                    Case 16
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(CDbl(0), xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .self_employed.nBusIncom = CDbl(loTxt.Text)
                    Case 17
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(CDbl(0), xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .self_employed.nMonExpns = CDbl(loTxt.Text)
                End Select
            End If
        End With
    End Sub

    Private Sub txtCoMak_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.comaker_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtCoMak" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .sLastName = loTxt.Text
                    Case 1
                        .sFrstName = loTxt.Text
                    Case 2
                        .sMiddName = loTxt.Text
                    Case 3
                        .sSuffixNm = loTxt.Text
                    Case 4
                        .sNickName = loTxt.Text
                    Case 5
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dBirthDte = CDate(loTxt.Text)
                    Case 9
                        .mobile_number(pnComak).sMobileNo = loTxt.Text
                        Call loadComakMobile()
                    Case 10
                        .sFBAcctxx = loTxt.Text
                End Select
            End If
        End With
    End Sub

    Private Sub txtPerso_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.applicant_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtPerso" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .sLastName = loTxt.Text
                    Case 1
                        .sFrstName = loTxt.Text
                    Case 2
                        .sMiddName = loTxt.Text
                    Case 3
                        .sSuffixNm = loTxt.Text
                    Case 4
                        .sNickName = loTxt.Text
                    Case 5
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dBirthDte = CDate(loTxt.Text)
                    Case 8
                        .mobile_number(pnMobile).sMobileNo = loTxt.Text
                        Call loadAppliMobile()
                    Case 11
                        .mobile_number(pnMobile).nPostYear = loTxt.Text
                        Call loadAppliMobile()
                    Case 14
                        .landline(pnLandline).sPhoneNox = loTxt.Text
                        Call loadAppliLandline()
                    Case 17
                        .email_address(pnEmail).sEmailAdd = loTxt.Text
                        Call loadAppliEmail()
                    Case 20
                        .facebook.sFBAcctxx = loTxt.Text
                    Case 21
                        .sVibeAcct = loTxt.Text
                    Case 22
                        .sMaidenNm = loTxt.Text
                    Case 24
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .facebook.nNoFriend = CInt(loTxt.Text)
                    Case 25
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .facebook.nYearxxxx = CInt(loTxt.Text)
                End Select
            End If
        End With
    End Sub

    Private Sub combobox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As ComboBox
        loChk = CType(sender, System.Windows.Forms.ComboBox)

        On Error Resume Next
        Dim lnIndex As Integer
        With p_oTrans.Detail
            lnIndex = Val(Mid(loChk.Name, 4))
            Select Case lnIndex
                Case 0
                    .cUnitAppl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 1
                    .cApplType = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 19
                    .disbursement_info.properties.with3Whls_info.cWithWhls = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 103
                    .disbursement_info.properties.with3Whls_info.cOwnerShp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 104
                    .disbursement_info.properties.with3Whls_info.cTermxxxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 105
                    .disbursement_info.properties.with3Whls_info.cStatusxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 60
                    .disbursement_info.properties.with4Whls_info.cWithWhls = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 100
                    .disbursement_info.properties.with4Whls_info.cOwnerShp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 101
                    .disbursement_info.properties.with4Whls_info.cTermxxxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 102
                    .disbursement_info.properties.with4Whls_info.cStatusxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 20
                    .disbursement_info.properties.with2Whls_info.cWithWhls = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 106
                    .disbursement_info.properties.with2Whls_info.cOwnerShp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 107
                    .disbursement_info.properties.with2Whls_info.cTermxxxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 108
                    .disbursement_info.properties.with2Whls_info.cStatusxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 24
                    .disbursement_info.bank_account.sAcctType = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 84
                    .spouse_info.residence_info.rent_others.cRntOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 80
                    .spouse_info.residence_info.cOwnershp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 81
                    .spouse_info.residence_info.cOwnOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 82
                    .spouse_info.residence_info.cGaragexx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 83
                    .spouse_info.residence_info.cHouseTyp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 26
                    .other_info.sUnitUser = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 27
                    .other_info.sUsr2Buyr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 28
                    .other_info.sPurposex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 29
                    .other_info.sUnitPayr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 30
                    .other_info.sPyr2Buyr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 33
                    .spouse_info.personal_info.cCvilStat = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 34
                    .spouse_info.personal_info.cGenderCd = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 35
                    .spouse_means.cIncmeSrc = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 36
                    .spouse_means.employed.cEmpSectr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 37
                    .spouse_means.employed.cUniforme = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 38
                    .spouse_means.employed.cMilitary = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 39
                    .spouse_means.employed.cGovtLevl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 40
                    .spouse_means.employed.cCompLevl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 41
                    .spouse_means.employed.cEmpLevlx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 42
                    .spouse_means.employed.cOcCatgry = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 43
                    .spouse_means.employed.cOFWRegnx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 97
                    If loChk.SelectedIndex.ToString = 0 Then
                        .spouse_means.employed.cEmpStatx = "R"
                    ElseIf loChk.SelectedIndex.ToString = 1 Then
                        .spouse_means.employed.cEmpStatx = "P"
                    ElseIf loChk.SelectedIndex.ToString = 2 Then
                        .spouse_means.employed.cEmpStatx = "C"
                    ElseIf loChk.SelectedIndex.ToString = 3 Then
                        .spouse_means.employed.cEmpStatx = "S"
                    ElseIf loChk.SelectedIndex.ToString = "-1" Then
                        .spouse_means.employed.cEmpStatx = ""
                    End If
                Case 98
                    .spouse_means.self_employed.cBusTypex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 99
                    .spouse_means.self_employed.cOwnTypex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 89
                    .spouse_means.self_employed.cOwnSizex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 70
                    .comaker_info.cIncmeSrc = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 71
                    .comaker_info.sReltnCde = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 3
                    .applicant_info.cCvilStat = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 4
                    .applicant_info.cGenderCd = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 62
                    .applicant_info.facebook.cAcctStat = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 90
                    .residence_info.cOwnershp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 5
                    .residence_info.cOwnOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 6
                    .residence_info.cGaragexx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 7
                    .residence_info.cHouseTyp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 8
                    .means_info.cIncmeSrc = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 9
                    .means_info.employed.cEmpSectr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 10
                    .means_info.employed.cUniforme = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 11
                    .means_info.employed.cMilitary = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 12
                    .means_info.employed.cGovtLevl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 13
                    .means_info.employed.cCompLevl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 14
                    .means_info.employed.cEmpLevlx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 15
                    .means_info.employed.cOcCatgry = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 16
                    .means_info.employed.cOFWRegnx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 96
                    If loChk.SelectedIndex.ToString = 0 Then
                        .means_info.employed.cEmpStatx = "R"
                    ElseIf loChk.SelectedIndex.ToString = 1 Then
                        .means_info.employed.cEmpStatx = "P"
                    ElseIf loChk.SelectedIndex.ToString = 2 Then
                        .means_info.employed.cEmpStatx = "C"
                    ElseIf loChk.SelectedIndex.ToString = 3 Then
                        .means_info.employed.cEmpStatx = "S"
                    ElseIf loChk.SelectedIndex.ToString = "-1" Then
                        .means_info.employed.cEmpStatx = ""
                    End If
                Case 17
                    .means_info.self_employed.cBusTypex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 18
                    .means_info.self_employed.cOwnTypex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 95
                    .means_info.self_employed.cOwnSizex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 85
                    .means_info.financed.sReltnCde = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 86
                    .means_info.pensioner.cPenTypex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 75
                    .spouse_info.personal_info.facebook.cAcctStat = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 91
                    .residence_info.rent_others.cRntOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 63
                    .disbursement_info.dependent_info.children(pnChild).sRelatnCD = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 64
                    .disbursement_info.dependent_info.children(pnChild).cIsPupilx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 65
                    .disbursement_info.dependent_info.children(pnChild).cIsPrivte = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 66
                    .disbursement_info.dependent_info.children(pnChild).sEducLevl = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 67
                    .disbursement_info.dependent_info.children(pnChild).cIsSchlrx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 68
                    .disbursement_info.dependent_info.children(pnChild).cHasWorkx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 69
                    .disbursement_info.dependent_info.children(pnChild).cWorkType = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 50
                    .disbursement_info.dependent_info.children(pnChild).cHouseHld = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 51
                    .disbursement_info.dependent_info.children(pnChild).cDependnt = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 52
                    .disbursement_info.dependent_info.children(pnChild).cIsChildx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 53
                    .disbursement_info.dependent_info.children(pnChild).cIsMarrdx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
            End Select
        End With
    End Sub

    Private Sub txtEmplo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.means_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtEmplo" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 1
                        .employed.sIndstWrk = loTxt.Text
                    Case 2
                        .employed.sEmployer = loTxt.Text
                    Case 3
                        .employed.sWrkAddrx = loTxt.Text
                    Case 6
                        .employed.sFunction = loTxt.Text
                    Case 8
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .employed.nLenServc = CInt(loTxt.Text)
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .employed.nSalaryxx = CDbl(loTxt.Text)
                    Case 10
                        .employed.sWrkTelno = loTxt.Text
                    Case 11
                        .self_employed.sIndstBus = loTxt.Text
                    Case 12
                        .self_employed.sBusiness = loTxt.Text
                    Case 13
                        .self_employed.sBusAddrx = loTxt.Text
                    Case 15
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .self_employed.nBusLenxx = CInt(loTxt.Text)
                    Case 16
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .self_employed.nBusIncom = CDbl(loTxt.Text)
                    Case 17
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .self_employed.nMonExpns = CDbl(loTxt.Text)
                    Case 18
                        .financed.sFinancer = loTxt.Text
                    Case 19
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .financed.nEstIncme = CDbl(loTxt.Text)
                    Case 21
                        .financed.sMobileNo = loTxt.Text
                    Case 22
                        .financed.sFBAcctxx = loTxt.Text
                    Case 23
                        .financed.sEmailAdd = loTxt.Text
                    Case 24
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .pensioner.nPensionx = CDbl(loTxt.Text)
                    Case 25
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .pensioner.nRetrYear = CInt(loTxt.Text)
                    Case 26
                        .other_income.sOthrIncm = loTxt.Text
                    Case 27
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .other_income.nOthrIncm = CDbl(loTxt.Text)
                End Select
            End If
        End With
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
    End Sub

    Private Sub initButton(ByVal fnValue As Integer)
        Dim lbShow As Integer
        lbShow = (fnValue = 1)

        Panel1.Enabled = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
        cmdButton00.Visible = Not lbShow
        cmdButton03.Visible = Not lbShow

        cmdButton02.Visible = lbShow
        cmdButton11.Visible = lbShow
        Panel1.Enabled = lbShow

        If fnValue = 1 Then
            cmb00.SelectedIndex = 4
        End If

    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))
        With p_oTrans
            Select Case lnIndex
                Case 3
                    If txtField00.Text <> "" Then
                        If p_oTrans.UpdateTransaction = True Then
                            initButton(1)
                        End If
                    End If
                Case 7 'Browse
                    If p_oTrans.SearchTransaction("%", False, False) = True Then
                        Call ClearFields(Me.Panel1)
                        Call ClearFields(Me.Panel2)
                        loadTransaction()
                    End If
                Case 8 'New
                    ClearFields(Me.Panel1)
                    ClearFields(Me.Panel2)
                    Call p_oTrans.NewTransaction()
                    Call loadTransaction()
                    initButton(1)
                Case 0 ' Exit
                    Me.Dispose()
                Case 2 'save
                    If p_oTrans.Detail.applicant_info.cCvilStat <> "1" And p_oTrans.Detail.applicant_info.cCvilStat <> "5" Then
                        If isWithSpouse(grpBox09) = False Then Exit Sub
                        If isWithSpouse(grpBox10) = False Then Exit Sub
                        If isWithSpouse(grpBox11) = False Then Exit Sub
                    End If

                    If DataComplete() = False Then Exit Sub

                    If MsgBox("Do you want to save this application??", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                        If p_oTrans.SaveTransaction Then
                            MsgBox("Application Successfully Save!!", vbInformation, "Information")
                            Call ClearFields(Me.Panel1)
                            Call ClearFields(Me.Panel2)
                            clearDependent()
                            initButton(0)
                        Else
                            MsgBox("Unable to save transaction.", vbInformation, "Information")
                            Call ClearFields(Me.Panel1)
                            Call ClearFields(Me.Panel2)
                            clearDependent()
                            initButton(0)
                        End If
                    End If

                Case 11 ' cancel
                    If MsgBox("Do you want to disregard all changes for this application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                        Call ClearFields(Me.Panel1)
                        Call ClearFields(Me.Panel2)
                        clearDependent()
                        initButton(0)
                    End If
                Case 4 'add
                    If Trim(dgv00.Rows(dgv00.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.disbursement_info.dependent_info.children.Add(New CARConst.children_param)
                        dgv00.Rows.Add()
                        clearDependent()
                        Call loadDependent_Info()
                        dgv00.CurrentCell = dgv00(0, Me.dgv00.RowCount - 1)
                        dgv00_Click(sender, New System.EventArgs())
                    End If

                Case 5 'delete
                    If dgv00.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.disbursement_info.dependent_info.children.RemoveAt(pnChild)
                            dgv00.CurrentCell = dgv00(0, 0)
                            dgv00_Click(sender, New System.EventArgs())
                            Call loadDependent_Info()
                        End If
                    End If
                Case 21
                    If Trim(dgv02.Rows(dgv02.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.applicant_info.mobile_number.Add(New CARConst.mobileno_param)
                        dgv02.Rows.Add()
                        Call loadAppliMobile()
                        dgv02.CurrentCell = dgv02(0, Me.dgv02.RowCount - 1)
                        dgv02_Click(sender, New System.EventArgs())
                    End If
                Case 22
                    If dgv02.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.applicant_info.mobile_number.RemoveAt(pnMobile)
                            dgv02.CurrentCell = dgv02(0, 0)
                            dgv02_Click(sender, New System.EventArgs())
                            Call loadAppliMobile()
                        End If
                    End If
                Case 31
                    If Trim(dgv03.Rows(dgv03.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.applicant_info.landline.Add(New CARConst.landline_param)
                        dgv03.Rows.Add()
                        Call loadAppliLandline()
                        dgv03.CurrentCell = dgv03(0, Me.dgv03.RowCount - 1)
                        dgv03_Click(sender, New System.EventArgs())
                    End If
                Case 32
                    If dgv03.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.applicant_info.landline.RemoveAt(pnLandline)
                            dgv03.CurrentCell = dgv03(0, 0)
                            dgv03_Click(sender, New System.EventArgs())
                            Call loadAppliLandline()
                        End If
                    End If
                Case 41
                    If Trim(dgv04.Rows(dgv04.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.applicant_info.email_address.Add(New CARConst.email_param)
                        dgv04.Rows.Add()
                        Call loadAppliEmail()
                        dgv04.CurrentCell = dgv04(0, Me.dgv04.RowCount - 1)
                        dgv04_Click(sender, New System.EventArgs())
                    End If
                Case 42
                    If dgv04.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.applicant_info.email_address.RemoveAt(pnEmail)
                            dgv04.CurrentCell = dgv04(0, 0)
                            dgv04_Click(sender, New System.EventArgs())
                            Call loadAppliEmail()
                        End If
                    End If

                Case 51
                    If Trim(dgv05.Rows(dgv05.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.spouse_info.personal_info.mobile_number.Add(New CARConst.mobileno_param)
                        dgv05.Rows.Add()
                        Call loadSpouseMobile()
                        dgv05.CurrentCell = dgv05(0, Me.dgv05.RowCount - 1)
                        dgv05_Click(sender, New System.EventArgs())
                    End If
                Case 52
                    If dgv05.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.spouse_info.personal_info.mobile_number.RemoveAt(pnSMobile)
                            dgv05.CurrentCell = dgv05(0, 0)
                            dgv05_Click(sender, New System.EventArgs())
                            Call loadSpouseMobile()
                        End If
                    End If

                Case 61
                    If Trim(dgv06.Rows(dgv06.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.spouse_info.personal_info.landline.Add(New CARConst.landline_param)
                        dgv06.Rows.Add()
                        Call loadSpouseLandline()
                        dgv06.CurrentCell = dgv06(0, Me.dgv06.RowCount - 1)
                        dgv06_Click(sender, New System.EventArgs())
                    End If
                Case 62
                    If dgv06.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.spouse_info.personal_info.email_address.RemoveAt(pnSLandLine)
                            dgv06.CurrentCell = dgv06(0, 0)
                            dgv06_Click(sender, New System.EventArgs())
                            Call loadSpouseLandline()
                        End If
                    End If

                Case 71
                    If Trim(dgv07.Rows(dgv07.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.spouse_info.personal_info.email_address.Add(New CARConst.email_param)
                        dgv07.Rows.Add()
                        Call loadSpouseEmail()
                        dgv07.CurrentCell = dgv07(0, Me.dgv07.RowCount - 1)
                        dgv07_Click(sender, New System.EventArgs())
                    End If
                Case 72
                    If dgv07.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.spouse_info.personal_info.email_address.RemoveAt(pnSEmail)
                            dgv07.CurrentCell = dgv07(0, 0)
                            dgv07_Click(sender, New System.EventArgs())
                            Call loadSpouseEmail()
                        End If
                    End If

                Case 23
                    If Trim(dgv01.Rows(dgv01.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.other_info.personal_reference.Add(New ggcGOCAS.CARConst.personal_reference_param)
                        dgv01.Rows.Add()
                        Call loadReference_Info()
                        dgv01.CurrentCell = dgv01(0, Me.dgv01.RowCount - 1)
                        dgv01_Click(sender, New System.EventArgs())
                    End If
                Case 24
                    If dgv01.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.other_info.personal_reference.RemoveAt(pnReference)
                            dgv01.CurrentCell = dgv01(0, 0)
                            dgv01_Click(sender, New System.EventArgs())
                            Call loadReference_Info()
                        End If
                    End If

                Case 81
                    If Trim(dgv08.Rows(dgv08.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Detail.comaker_info.mobile_number.Add(New ggcGOCAS.CARConst.mobileno_param)
                        dgv08.Rows.Add()
                        Call loadComakMobile()
                        dgv08.CurrentCell = dgv08(0, Me.dgv08.RowCount - 1)
                        dgv08_Click(sender, New System.EventArgs())
                    End If
                Case 82
                    If dgv08.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Detail.comaker_info.mobile_number.RemoveAt(pnComak)
                            dgv08.CurrentCell = dgv08(0, 0)
                            dgv08_Click(sender, New System.EventArgs())
                            Call loadComakMobile()
                        End If
                    End If
            End Select
        End With
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

    Private Sub loadTransaction()
        Call loadMainInfo(Me.grpBox14)
        Call loadIntroQuestion(Me.grpBox00)
        Call loadAppliInfo(Me.grpBox02)
        Call loadAppliRes(Me.grpBox03)
        Call loadAppEmplymnt(Me.grpBox04)
        Call loadAppEmplymnt(Me.grpBox05)
        Call loadDisburesement(Me.grpBox06)
        Call loadDisburesement(Me.grpBox07)
        Call loadOthers(Me.grpBox08)
        Call loadSpouseInfo(Me.grpBox09)
        Call loadSpouseRes(Me.grpBox10)
        Call loadSpouseEmpl(Me.grpBox11)
        Call loadComaker(Me.grpBox12)
        Call setTranStat(IFNull(p_oTrans.Master("cTranStat"), "-1"), lblStatus)
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

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 80, 81
                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 80, True, False), False) = True Then
                            Call ClearFields(Me.Panel1)
                            Call ClearFields(Me.Panel2)
                            loadTransaction()
                        End If
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtOther" Then
                Select Case loIndex
                    Case 3
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.other_info.personal_reference(pnReference).sRefrTown)
                        Call loadReference_Info()
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtIntro" Then
                Select Case loIndex
                    Case 0
                        loTxt.Text = p_oTrans.getModel(loTxt.Text, True, False, p_oTrans.Detail.sModelIDx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtPerso" Then
                Select Case loIndex
                    Case 6
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.applicant_info.sBirthPlc)
                    Case 7
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Detail.applicant_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtResid" Then
                Select Case loIndex
                    Case 5
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.residence_info.present_address.sTownIDxx)
                    Case 6
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Detail.residence_info.present_address.sBrgyIDxx, p_oTrans.Detail.residence_info.present_address.sTownIDxx)
                    Case 16
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.residence_info.permanent_address.sTownIDxx)
                    Case 17
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Detail.residence_info.permanent_address.sBrgyIDxx, p_oTrans.Detail.residence_info.permanent_address.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtCoMak" Then
                Select Case loIndex
                    Case 6
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.comaker_info.sBirthPlc)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoIn" Then
                Select Case loIndex
                    Case 6
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.spouse_info.personal_info.sBirthPlc)
                    Case 7
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Detail.spouse_info.personal_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoRe" Then
                Select Case loIndex
                    Case 4
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.spouse_info.residence_info.present_address.sTownIDxx)
                    Case 5
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Detail.spouse_info.residence_info.present_address.sBrgyIDxx, p_oTrans.Detail.spouse_info.residence_info.present_address.sTownIDxx)
                    Case 13
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.spouse_info.residence_info.permanent_address.sTownIDxx)
                    Case 14
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Detail.spouse_info.residence_info.permanent_address.sBrgyIDxx, p_oTrans.Detail.spouse_info.residence_info.permanent_address.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoEm" Then
                Select Case loIndex
                    Case 3
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.spouse_means.employed.sWrkTownx)
                    Case 4
                        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Detail.spouse_means.employed.sPosition)
                    Case 13
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.spouse_means.self_employed.sBusTownx)
                    Case 20
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Detail.spouse_means.employed.sOFWNatnx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtEmplo" Then
                Select Case loIndex
                    Case 0
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Detail.means_info.employed.sOFWNatnx)
                    Case 5
                        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Detail.means_info.employed.sPosition)
                    Case 4
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.means_info.employed.sWrkTownx)
                    Case 14
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.means_info.self_employed.sBusTownx)
                    Case 20
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Detail.means_info.financed.sNatnCode)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtDisbu" Then
                Select Case loIndex
                    Case 16
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Detail.disbursement_info.dependent_info.children(pnChild).sSchlTown)
                        Call loadDependent_Info()
                End Select
            End If

            '*********************
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
                If (TypeOf loTxt Is TextBox) Then
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtCoMak" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtIntro" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtPerso" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtResid" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtDisbu" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtOther" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoIn" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoRe" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Then
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
        Next
        pnMobile = 0
        pnEmail = 0
        pnLandline = 0
        pnChild = 0
        pnReference = 0
        pnSMobile = 0
        pnSLandLine = 0
        pnSEmail = 0
        pnComak = 0
        chk00.Checked = False
        chk01.Checked = False
        chk02.Checked = False
        setTranStat("-1", lblStatus)
        tabControl00.SelectedIndex = 0
    End Sub

    Private Sub loadIntroQuestion(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadIntroQuestion(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtIntro" Then
                        With p_oTrans.Detail
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")
                                Case 1
                                    loTxt.Text = Format(CDbl(.nDownPaym), xsDECIMAL)
                                Case 2
                                    loTxt.Text = CInt(.nAcctTerm)
                                Case 3
                                    loTxt.Text = Format(CDbl(.nMonAmort), xsDECIMAL)
                                Case 4
                                    loTxt.Text = .sUnitAppl
                                Case 5
                                    loTxt.Text = p_oTrans.getBranch(.sBranchCd, False, True, "")
                                Case 6
                                    If Not IsDate(.dTargetDt) Then .dTargetDt = p_oAppDriver.SysDate
                                    loTxt.Text = Format(CDate(.dTargetDt), xsDATE_MEDIUM)
                                Case 7
                                    loTxt.Text = Format(CDbl(.nRebatesx), xsDECIMAL)
                                Case 8
                                    loTxt.Text = Format(CDbl(.nPNValuex), xsDECIMAL)
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.cUnitAppl <> "") Then setApplicationType(p_oTrans.Detail.cUnitAppl, cmb00)
        If (p_oTrans.Detail.cApplType <> "") Then setTypeOfCustomer(p_oTrans.Detail.cApplType, cmb01)
    End Sub

    Private Sub loadMainInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMainInfo(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                        With p_oTrans
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = IFNull(.Master("sTransNox"), "")
                                Case 2
                                    loTxt.Text = Format(IFNull(.Master("dTransact"), p_oAppDriver.getSysDate), xsDATE_MEDIUM)
                            End Select
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub loadAppliInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadAppliInfo(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtPerso" Then
                        With p_oTrans.Detail.applicant_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName
                                Case 1
                                    loTxt.Text = .sFrstName
                                Case 2
                                    loTxt.Text = .sMiddName
                                Case 3
                                    loTxt.Text = .sSuffixNm
                                Case 4
                                    loTxt.Text = .sNickName
                                Case 5
                                    If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                    loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                Case 6
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 7
                                    loTxt.Text = p_oTrans.getCountry(.sCitizenx, False, True, "")
                                Case 20
                                    loTxt.Text = .facebook.sFBAcctxx
                                Case 21
                                    loTxt.Text = .sVibeAcct
                                Case 22
                                    loTxt.Text = .sMaidenNm
                                Case 24
                                    loTxt.Text = .facebook.nNoFriend
                                Case 25
                                    loTxt.Text = .facebook.nYearxxxx
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.applicant_info.cCvilStat <> "") Then setCivilStat(p_oTrans.Detail.applicant_info.cCvilStat, cmb03)
        If (p_oTrans.Detail.applicant_info.cGenderCd <> "") Then setGender(p_oTrans.Detail.applicant_info.cGenderCd, cmb04)
        If (p_oTrans.Detail.applicant_info.facebook.cAcctStat <> "") Then setAccountStatus(p_oTrans.Detail.applicant_info.facebook.cAcctStat, cmb62)
        Call loadAppliMobile()
        Call loadAppliLandline()
        Call loadAppliEmail()
    End Sub

    Private Sub loadAppliMobile()
        With dgv02
            .Rows.Clear()
            If p_oTrans.Detail.applicant_info.mobile_number.Count = 0 Then
                p_oTrans.Detail.applicant_info.mobile_number.Add(New CARConst.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.applicant_info.mobile_number.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.applicant_info.mobile_number(lnCtr).sMobileNo
                Select Case p_oTrans.Detail.applicant_info.mobile_number(lnCtr).cPostPaid
                    Case 1
                        .Rows(lnCtr).Cells(2).Value = "Yes"
                    Case Else
                        .Rows(lnCtr).Cells(2).Value = "No"
                End Select
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv02.CurrentCell = dgv02.Rows(dgv02.RowCount - 1).Cells(0)
        dgv02.Rows(dgv02.RowCount - 1).Selected = True
    End Sub

    Private Sub loadAppliLandline()
        With dgv03
            .Rows.Clear()
            If p_oTrans.Detail.applicant_info.landline.Count = 0 Then
                p_oTrans.Detail.applicant_info.landline.Add(New CARConst.landline_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.applicant_info.landline.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.applicant_info.landline(lnCtr).sPhoneNox
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv03.CurrentCell = dgv03.Rows(dgv03.RowCount - 1).Cells(0)
        dgv03.Rows(dgv03.RowCount - 1).Selected = True
    End Sub

    Private Sub loadAppliEmail()
        With dgv04
            .Rows.Clear()
            If p_oTrans.Detail.applicant_info.email_address.Count = 0 Then
                p_oTrans.Detail.applicant_info.email_address.Add(New CARConst.email_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.applicant_info.email_address.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.applicant_info.email_address(lnCtr).sEmailAdd
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv04.CurrentCell = dgv04.Rows(dgv04.RowCount - 1).Cells(0)
        dgv04.Rows(dgv04.RowCount - 1).Selected = True
    End Sub

    Private Sub loadSpouseMobile()
        With dgv05
            .Rows.Clear()
            If p_oTrans.Detail.spouse_info.personal_info.mobile_number.Count = 0 Then
                p_oTrans.Detail.spouse_info.personal_info.mobile_number.Add(New CARConst.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.spouse_info.personal_info.mobile_number.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.spouse_info.personal_info.mobile_number(lnCtr).sMobileNo
                Select Case p_oTrans.Detail.spouse_info.personal_info.mobile_number(lnCtr).cPostPaid
                    Case 1
                        .Rows(lnCtr).Cells(2).Value = "Yes"
                    Case Else
                        .Rows(lnCtr).Cells(2).Value = "No"
                End Select
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv05.CurrentCell = dgv05.Rows(dgv05.RowCount - 1).Cells(0)
        dgv05.Rows(dgv05.RowCount - 1).Selected = True
    End Sub

    Private Sub loadComakMobile()
        With dgv08
            .Rows.Clear()
            If p_oTrans.Detail.comaker_info.mobile_number.Count = 0 Then
                p_oTrans.Detail.comaker_info.mobile_number.Add(New CARConst.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.comaker_info.mobile_number.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.comaker_info.mobile_number(lnCtr).sMobileNo
                Select Case p_oTrans.Detail.comaker_info.mobile_number(lnCtr).cPostPaid
                    Case 1
                        .Rows(lnCtr).Cells(2).Value = "Yes"
                    Case Else
                        .Rows(lnCtr).Cells(2).Value = "No"
                End Select
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv08.CurrentCell = dgv08.Rows(dgv08.RowCount - 1).Cells(0)
        dgv08.Rows(dgv08.RowCount - 1).Selected = True
    End Sub

    Private Sub loadSpouseLandline()
        With dgv06
            .Rows.Clear()
            If p_oTrans.Detail.spouse_info.personal_info.landline.Count = 0 Then
                p_oTrans.Detail.spouse_info.personal_info.landline.Add(New CARConst.landline_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.spouse_info.personal_info.landline.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.spouse_info.personal_info.landline(lnCtr).sPhoneNox
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv06.CurrentCell = dgv06.Rows(dgv06.RowCount - 1).Cells(0)
        dgv06.Rows(dgv06.RowCount - 1).Selected = True
    End Sub

    Private Sub loadSpouseEmail()
        With dgv07
            .Rows.Clear()
            If p_oTrans.Detail.spouse_info.personal_info.email_address.Count = 0 Then
                p_oTrans.Detail.spouse_info.personal_info.email_address.Add(New CARConst.email_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.spouse_info.personal_info.email_address.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.spouse_info.personal_info.email_address(lnCtr).sEmailAdd
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv07.CurrentCell = dgv07.Rows(dgv07.RowCount - 1).Cells(0)
        dgv07.Rows(dgv07.RowCount - 1).Selected = True
    End Sub


    Private Sub loadOthers(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadOthers(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtOther" Then
                        With p_oTrans.Detail.other_info
                            Select Case loIndex
                                Case 4
                                    loTxt.Text = .sSrceInfo
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.other_info.sUnitUser <> "") Then setUnitUser(p_oTrans.Detail.other_info.sUnitUser, cmb26)
        If (p_oTrans.Detail.other_info.sUsr2Buyr <> "") Then setUserBuyer(p_oTrans.Detail.other_info.sUsr2Buyr, cmb27)
        If (p_oTrans.Detail.other_info.sPurposex <> "") Then setPurpose(p_oTrans.Detail.other_info.sPurposex, cmb28)
        If (p_oTrans.Detail.other_info.sUnitPayr <> "") Then setUnitPayor(p_oTrans.Detail.other_info.sUnitPayr, cmb29)
        If (p_oTrans.Detail.other_info.sPyr2Buyr <> "") Then setUnitPayr2(p_oTrans.Detail.other_info.sPyr2Buyr, cmb30)
        Call loadReference_Info()
    End Sub

    Private Sub loadDisburesement(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadDisburesement(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtDisbu" Then
                        With p_oTrans.Detail.disbursement_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nElctrcBl), xsDECIMAL)
                                Case 1
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nWaterBil), xsDECIMAL)
                                Case 2
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nFoodAllw), xsDECIMAL)
                                Case 3
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nLoanAmtx), xsDECIMAL)
                                Case 4
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nEductnxx), xsDECIMAL)
                                Case 7
                                    loTxt.Text = .bank_account.sBankName
                                Case 8
                                    loTxt.Text = .credit_card.sBankName
                                Case 9
                                    loTxt.Text = Format(CDbl(.credit_card.nCrdLimit), xsDECIMAL)
                                Case 10
                                    loTxt.Text = .credit_card.nSinceYrx
                                Case 11
                                    loTxt.Text = .dependent_info.nHouseHld
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cWithWhls <> "") Then setisIwithWheel(p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cWithWhls, cmb60)
        If (p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cOwnerShp <> "") Then setWheelsOwnership(p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cOwnerShp, cmb100)
        If (p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cTermxxxx <> "") Then setWheelsTerm(p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cTermxxxx, cmb101)
        If (p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cStatusxx <> "") Then setWheelsActStat(p_oTrans.Detail.disbursement_info.properties.with4Whls_info.cStatusxx, cmb102)
        If (p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cWithWhls <> "") Then setisIwithWheel(p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cWithWhls, cmb19)
        If (p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cOwnerShp <> "") Then setWheelsOwnership(p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cOwnerShp, cmb103)
        If (p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cTermxxxx <> "") Then setWheelsTerm(p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cTermxxxx, cmb104)
        If (p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cStatusxx <> "") Then setWheelsActStat(p_oTrans.Detail.disbursement_info.properties.with3Whls_info.cStatusxx, cmb105)
        If (p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cWithWhls <> "") Then setisIwithWheel(p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cWithWhls, cmb20)
        If (p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cOwnerShp <> "") Then setWheelsOwnership(p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cOwnerShp, cmb106)
        If (p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cTermxxxx <> "") Then setWheelsTerm(p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cTermxxxx, cmb107)
        If (p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cStatusxx <> "") Then setWheelsActStat(p_oTrans.Detail.disbursement_info.properties.with2Whls_info.cStatusxx, cmb108)
        If (p_oTrans.Detail.disbursement_info.bank_account.sAcctType <> "") Then setBankType(p_oTrans.Detail.disbursement_info.bank_account.sAcctType, cmb24)
        Call loadDependent_Info()
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtCoMak" Then
                        With p_oTrans.Detail.comaker_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName
                                Case 1
                                    loTxt.Text = .sFrstName
                                Case 2
                                    loTxt.Text = .sMiddName
                                Case 3
                                    loTxt.Text = .sSuffixNm
                                Case 4
                                    loTxt.Text = .sNickName
                                Case 5
                                    If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                    loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                Case 6
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 10
                                    loTxt.Text = .sFBAcctxx
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.comaker_info.cIncmeSrc <> "") Then setIncomeSource(p_oTrans.Detail.comaker_info.cIncmeSrc, cmb70)
        If (p_oTrans.Detail.comaker_info.sReltnCde <> "") Then setFinanceType(p_oTrans.Detail.comaker_info.sReltnCde, cmb71)
        Call loadComakMobile()
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
                        With p_oTrans.Detail.spouse_info.personal_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName
                                Case 1
                                    loTxt.Text = .sFrstName
                                Case 2
                                    loTxt.Text = .sSuffixNm
                                Case 3
                                    loTxt.Text = .sMiddName
                                Case 4
                                    loTxt.Text = .sNickName
                                Case 5
                                    If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                    loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                Case 6
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 7
                                    loTxt.Text = p_oTrans.getCountry(.sCitizenx, False, True, "")
                                Case 14
                                    loTxt.Text = .sMaidenNm
                                Case 18
                                    loTxt.Text = .facebook.sFBAcctxx
                                Case 20
                                    loTxt.Text = .facebook.cAcctStat
                                Case 21
                                    loTxt.Text = .facebook.nNoFriend
                                Case 22
                                    loTxt.Text = .facebook.nYearxxxx
                                Case 19
                                    loTxt.Text = .sVibeAcct
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.spouse_info.personal_info.cCvilStat <> "") Then setCivilStat(p_oTrans.Detail.spouse_info.personal_info.cCvilStat, cmb33)
        If (p_oTrans.Detail.spouse_info.personal_info.facebook.cAcctStat <> "") Then setAccountStatus(p_oTrans.Detail.spouse_info.personal_info.facebook.cAcctStat, cmb75)
        If (p_oTrans.Detail.spouse_info.personal_info.cGenderCd <> "") Then setGender(p_oTrans.Detail.spouse_info.personal_info.cGenderCd, cmb34)
        Call loadSpouseMobile()
        Call loadSpouseLandline()
        Call loadSpouseEmail()
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoRe" Then
                        With p_oTrans.Detail.spouse_info.residence_info
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
                                    loTxt.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                Case 5
                                    loTxt.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                Case 6
                                    If (Not IsNothing(.rent_others)) Then loTxt.Text = .rent_others.nLenStayx
                                Case 7
                                    If Not IsNothing(.rent_others) Then loTxt.Text = .rent_others.nRentExps
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
                                    loTxt.Text = p_oTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                Case 14
                                    loTxt.Text = p_oTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.spouse_info.residence_info.cOwnershp <> "") Then setOwnership(p_oTrans.Detail.spouse_info.residence_info.cOwnershp, cmb80)
        If (p_oTrans.Detail.spouse_info.residence_info.cOwnOther <> "") Then setOwnedOther(p_oTrans.Detail.spouse_info.residence_info.cOwnOther, cmb81)
        If (p_oTrans.Detail.spouse_info.residence_info.cGaragexx <> "") Then setGarage(p_oTrans.Detail.spouse_info.residence_info.cGaragexx, cmb82)
        If (p_oTrans.Detail.spouse_info.residence_info.cHouseTyp <> "") Then setHouseType(p_oTrans.Detail.spouse_info.residence_info.cHouseTyp, cmb83)
        If (p_oTrans.Detail.spouse_info.residence_info.rent_others.cRntOther <> "") Then setRent(p_oTrans.Detail.spouse_info.residence_info.rent_others.cRntOther, cmb84)
    End Sub

    Private Sub loadSpouseEmpl(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseEmpl(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Then
                        With p_oTrans.Detail.spouse_means
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .employed.sIndstWrk
                                Case 1
                                    loTxt.Text = .employed.sEmployer
                                Case 2
                                    loTxt.Text = .employed.sWrkAddrx
                                Case 3
                                    loTxt.Text = p_oTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                Case 4
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                Case 5
                                    loTxt.Text = .employed.sFunction
                                Case 7
                                    loTxt.Text = .employed.nLenServc
                                Case 8
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                Case 9
                                    loTxt.Text = .employed.sWrkTelno
                                Case 10
                                    loTxt.Text = .self_employed.sIndstBus
                                Case 11
                                    loTxt.Text = .self_employed.sBusiness
                                Case 12
                                    loTxt.Text = .self_employed.sBusAddrx
                                Case 13
                                    loTxt.Text = p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                Case 15
                                    loTxt.Text = .self_employed.nBusLenxx
                                Case 16
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                                Case 17
                                    loTxt.Text = Format(CDbl(.self_employed.nMonExpns), xsDECIMAL)
                                Case 20
                                    loTxt.Text = p_oTrans.getCountry(.employed.sOFWNatnx, False, True, "")
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.spouse_means.cIncmeSrc <> "") Then setIncomeSource(p_oTrans.Detail.spouse_means.cIncmeSrc, cmb35)
        If (p_oTrans.Detail.spouse_means.employed.cEmpSectr <> "") Then setEmploymentSector(p_oTrans.Detail.spouse_means.employed.cEmpSectr, cmb36)
        If (p_oTrans.Detail.spouse_means.employed.cUniforme <> "") Then setIsUniformed(p_oTrans.Detail.spouse_means.employed.cUniforme, cmb37)
        If (p_oTrans.Detail.spouse_means.employed.cMilitary <> "") Then setIsMilitaryUniformed(p_oTrans.Detail.spouse_means.employed.cMilitary, cmb38)
        If (p_oTrans.Detail.spouse_means.employed.cGovtLevl <> "") Then setGovernmentLevel(p_oTrans.Detail.spouse_means.employed.cGovtLevl, cmb39)
        If (p_oTrans.Detail.spouse_means.employed.cCompLevl <> "") Then setCompanyLevel(p_oTrans.Detail.spouse_means.employed.cCompLevl, cmb40)
        If (p_oTrans.Detail.spouse_means.employed.cEmpLevlx <> "") Then setEmploymentLevel(p_oTrans.Detail.spouse_means.employed.cEmpLevlx, cmb41)
        If (p_oTrans.Detail.spouse_means.employed.cOcCatgry <> "") Then setOccptCateg(p_oTrans.Detail.spouse_means.employed.cOcCatgry, cmb42)
        If (p_oTrans.Detail.spouse_means.employed.cOFWRegnx <> "") Then setOFReg(p_oTrans.Detail.spouse_means.employed.cOFWRegnx, cmb43)
        If (p_oTrans.Detail.spouse_means.employed.cEmpStatx <> "") Then setStatEmployment(p_oTrans.Detail.spouse_means.employed.cEmpStatx, cmb97)
        If (p_oTrans.Detail.spouse_means.self_employed.cBusTypex <> "") Then setBusinessOwnership(p_oTrans.Detail.spouse_means.self_employed.cBusTypex, cmb98)
        If (p_oTrans.Detail.spouse_means.self_employed.cOwnTypex <> "") Then setBusinessOwnership(p_oTrans.Detail.spouse_means.self_employed.cOwnTypex, cmb99)
        If (p_oTrans.Detail.spouse_means.self_employed.cOwnSizex <> "") Then setBusinessSize(p_oTrans.Detail.spouse_means.self_employed.cOwnSizex, cmb89)
    End Sub

    Private Sub loadAppliRes(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadAppliRes(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtResid" Then
                        With p_oTrans.Detail.residence_info
                            Select Case loIndex
                                Case 1
                                    loTxt.Text = .present_address.sLandMark
                                Case 2
                                    loTxt.Text = .present_address.sHouseNox
                                Case 3
                                    loTxt.Text = .present_address.sAddress1
                                Case 4
                                    loTxt.Text = .present_address.sAddress2
                                Case 5
                                    loTxt.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                Case 6
                                    loTxt.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                Case 9
                                    loTxt.Text = IIf(.rent_others.nLenStayx = "", "", CInt(.rent_others.nLenStayx))
                                Case 10
                                    loTxt.Text = IIf(.rent_others.nRentExps = "", "", CDbl(.rent_others.nRentExps))
                                Case 11
                                    loTxt.Text = .sCtkReltn
                                Case 12
                                    loTxt.Text = .permanent_address.sLandMark
                                Case 13
                                    loTxt.Text = .permanent_address.sHouseNox
                                Case 14
                                    loTxt.Text = .permanent_address.sAddress1
                                Case 15
                                    loTxt.Text = .permanent_address.sAddress2
                                Case 16
                                    loTxt.Text = p_oTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                Case 17
                                    loTxt.Text = p_oTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.residence_info.cOwnershp <> "") Then setOwnership(p_oTrans.Detail.residence_info.cOwnershp, cmb90)
        If (p_oTrans.Detail.residence_info.cOwnOther <> "") Then setOwnedOther(p_oTrans.Detail.residence_info.cOwnOther, cmb05)
        If (p_oTrans.Detail.residence_info.rent_others.cRntOther <> "") Then setRent(p_oTrans.Detail.residence_info.rent_others.cRntOther, cmb91)
        If (p_oTrans.Detail.residence_info.cHouseTyp <> "") Then setHouseType(p_oTrans.Detail.residence_info.cHouseTyp, cmb07)
        If (p_oTrans.Detail.residence_info.cGaragexx <> "") Then setGarage(p_oTrans.Detail.residence_info.cGaragexx, cmb06)
    End Sub

    Private Sub loadAppEmplymnt(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadAppEmplymnt(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Then
                        With p_oTrans.Detail.means_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = p_oTrans.getCountry(.employed.sOFWNatnx, False, True, "")
                                Case 1
                                    loTxt.Text = .employed.sIndstWrk
                                Case 2
                                    loTxt.Text = .employed.sEmployer
                                Case 3
                                    loTxt.Text = .employed.sWrkAddrx
                                Case 4
                                    loTxt.Text = p_oTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                Case 5
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                Case 6
                                    loTxt.Text = .employed.sFunction
                                Case 8
                                    loTxt.Text = .employed.nLenServc
                                Case 9
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                Case 10
                                    loTxt.Text = .employed.sWrkTelno
                                Case 11
                                    loTxt.Text = .self_employed.sIndstBus
                                Case 12
                                    loTxt.Text = .self_employed.sBusiness
                                Case 13
                                    loTxt.Text = .self_employed.sBusAddrx
                                Case 14
                                    loTxt.Text = p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                Case 15
                                    loTxt.Text = .self_employed.nBusLenxx
                                Case 16
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                                Case 17
                                    loTxt.Text = Format(CDbl(.self_employed.nMonExpns), xsDECIMAL)
                                Case 18
                                    loTxt.Text = .financed.sFinancer
                                Case 19
                                    loTxt.Text = Format(CDbl(.financed.nEstIncme), xsDECIMAL)
                                Case 20
                                    loTxt.Text = p_oTrans.getCountry(.financed.sNatnCode, False, True, "")
                                Case 21
                                    loTxt.Text = .financed.sMobileNo
                                Case 22
                                    loTxt.Text = .financed.sFBAcctxx
                                Case 23
                                    loTxt.Text = .financed.sEmailAdd
                                Case 24
                                    loTxt.Text = Format(CDbl(.pensioner.nPensionx), xsDECIMAL)
                                Case 25
                                    If Not IsNumeric(.other_income.nOthrIncm) Then .pensioner.nRetrYear = 0
                                    loTxt.Text = CInt(.pensioner.nRetrYear)
                                Case 26
                                    loTxt.Text = .other_income.sOthrIncm
                                Case 27
                                    If Not IsNumeric(.other_income.nOthrIncm) Then .other_income.nOthrIncm = 0
                                    loTxt.Text = Format(CDbl(.other_income.nOthrIncm), xsDECIMAL)
                            End Select
                        End With
                    End If
                End If
            End If
        Next
        If (p_oTrans.Detail.means_info.cIncmeSrc <> "") Then setIncomeSource(p_oTrans.Detail.means_info.cIncmeSrc, cmb08)
        If (p_oTrans.Detail.means_info.employed.cEmpSectr <> "") Then setEmploymentSector(p_oTrans.Detail.means_info.employed.cEmpSectr, cmb09)
        If (p_oTrans.Detail.means_info.employed.cUniforme <> "") Then setIsUniformed(p_oTrans.Detail.means_info.employed.cUniforme, cmb10)
        If (p_oTrans.Detail.means_info.employed.cMilitary <> "") Then setIsMilitaryUniformed(p_oTrans.Detail.means_info.employed.cMilitary, cmb11)
        If (p_oTrans.Detail.means_info.employed.cGovtLevl <> "") Then setGovernmentLevel(p_oTrans.Detail.means_info.employed.cGovtLevl, cmb12)
        If (p_oTrans.Detail.means_info.employed.cCompLevl <> "") Then setCompanyLevel(p_oTrans.Detail.means_info.employed.cCompLevl, cmb13)
        If (p_oTrans.Detail.means_info.employed.cEmpLevlx <> "") Then setEmploymentLevel(p_oTrans.Detail.means_info.employed.cEmpLevlx, cmb14)
        If (p_oTrans.Detail.means_info.employed.cOcCatgry <> "") Then setOccptCateg(p_oTrans.Detail.means_info.employed.cOcCatgry, cmb15)
        If (p_oTrans.Detail.means_info.employed.cOFWRegnx <> "") Then setOFReg(p_oTrans.Detail.means_info.employed.cOFWRegnx, cmb16)
        If (p_oTrans.Detail.means_info.employed.cEmpStatx <> "") Then setStatEmployment(p_oTrans.Detail.means_info.employed.cEmpStatx, cmb96)
        If (p_oTrans.Detail.means_info.self_employed.cBusTypex <> "") Then setBusinessOwnership(p_oTrans.Detail.means_info.self_employed.cBusTypex, cmb17)
        If (p_oTrans.Detail.means_info.self_employed.cOwnTypex <> "") Then setBusinessOwnership(p_oTrans.Detail.means_info.self_employed.cOwnTypex, cmb18)
        If (p_oTrans.Detail.means_info.self_employed.cOwnSizex <> "") Then setBusinessSize(p_oTrans.Detail.means_info.self_employed.cOwnSizex, cmb95)
        If (p_oTrans.Detail.means_info.financed.sReltnCde <> "") Then setFinanceType(p_oTrans.Detail.means_info.financed.sReltnCde, cmb85)
        If (p_oTrans.Detail.means_info.pensioner.cPenTypex <> "") Then setPensionType(p_oTrans.Detail.means_info.pensioner.cPenTypex, cmb86)
    End Sub

    Public Sub showReference(ByVal fnRow As Integer)
        If p_oTrans.Detail.other_info.personal_reference.Count = 0 Then Exit Sub
        With p_oTrans.Detail.other_info
            txtOther00.Text = .personal_reference(fnRow).sRefrNmex
            txtOther01.Text = .personal_reference(fnRow).sRefrMPNx
            txtOther02.Text = .personal_reference(fnRow).sRefrAddx
            txtOther03.Text = p_oTrans.getTownCity(.personal_reference(fnRow).sRefrTown, False, True, "")
        End With
        pnReference = fnRow
    End Sub

    Public Sub loadDependent_Info()
        With dgv00
            .Rows.Clear()
            If p_oTrans.Detail.disbursement_info.dependent_info.children.Count = 0 Then
                p_oTrans.Detail.disbursement_info.dependent_info.children.Add(New CARConst.children_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.disbursement_info.dependent_info.children.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.disbursement_info.dependent_info.children(lnCtr).sFullName
                .Rows(lnCtr).Cells(2).Value = p_oTrans.Detail.disbursement_info.dependent_info.children(lnCtr).nDepdAgex
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv00.CurrentCell = dgv00.Rows(dgv00.RowCount - 1).Cells(0)
        dgv00.Rows(dgv00.RowCount - 1).Selected = True
    End Sub

    Private Sub loadReference_Info()
        With dgv01
            .Rows.Clear()
            If p_oTrans.Detail.other_info.personal_reference.Count = 0 Then
                p_oTrans.Detail.other_info.personal_reference.Add(New CARConst.personal_reference_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Detail.other_info.personal_reference.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Detail.other_info.personal_reference(lnCtr).sRefrNmex
                .Rows(lnCtr).Cells(2).Value = p_oTrans.Detail.other_info.personal_reference(lnCtr).sRefrMPNx
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv01.CurrentCell = dgv01.Rows(dgv01.RowCount - 1).Cells(0)
        dgv01.Rows(dgv01.RowCount - 1).Selected = True
    End Sub

    Private Sub showDependentInfo(ByVal fnRow As Integer)
        If p_oTrans.Detail.disbursement_info.dependent_info.children.Count = 0 Then Exit Sub
        With p_oTrans.Detail.disbursement_info.dependent_info
            If .children(fnRow).sFullName <> "" Then
                txtDisbu12.Text = .children(fnRow).sFullName
                txtDisbu13.Text = .children(fnRow).nDepdAgex
                txtDisbu14.Text = .children(fnRow).sSchlName
                txtDisbu15.Text = .children(fnRow).sSchlAddr
                txtDisbu16.Text = p_oTrans.getTownCity(.children(fnRow).sSchlTown, False, True, "")
                txtDisbu17.Text = .children(fnRow).sCompanyx

                setRel(.children(fnRow).sRelatnCD, cmb63)
                setIsStudent(.children(fnRow).cIsPupilx, cmb64)
                setIsPrivate(.children(fnRow).cIsPrivte, cmb65)
                setEducLevel(.children(fnRow).sEducLevl, cmb66)
                setIsScholar(.children(fnRow).cIsSchlrx, cmb67)
                setHasWorked(.children(fnRow).cHasWorkx, cmb68)
                setEmpSector(.children(fnRow).cWorkType, cmb69)
                setIsHousehold(.children(fnRow).cHouseHld, cmb50)
                setIsDependent(.children(fnRow).cDependnt, cmb51)
                setIsChild(.children(fnRow).cIsChildx, cmb52)
                setIsMarried(.children(fnRow).cIsMarrdx, cmb53)
            Else
                txtDisbu12.Text = ""
                txtDisbu13.Text = ""
                txtDisbu14.Text = ""
                txtDisbu15.Text = ""
                txtDisbu16.Text = ""
                txtDisbu17.Text = ""
                cmb63.SelectedIndex = -1
                cmb64.SelectedIndex = -1
                cmb65.SelectedIndex = -1
                cmb66.SelectedIndex = -1
                cmb67.SelectedIndex = -1
                cmb68.SelectedIndex = -1
                cmb69.SelectedIndex = -1
                cmb50.SelectedIndex = -1
                cmb51.SelectedIndex = -1
                cmb52.SelectedIndex = -1
                cmb53.SelectedIndex = -1
            End If
        End With
        pnChild = fnRow
    End Sub

    Public Sub showAppliMobile(ByVal fnRow As Integer)
        If p_oTrans.Detail.applicant_info.mobile_number.Count = 0 Then Exit Sub
        With p_oTrans.Detail.applicant_info
            txtPerso08.Text = .mobile_number(fnRow).sMobileNo
            chk00.Checked = IIf(.mobile_number(fnRow).cPostPaid <> "1", False, True)
        End With
        pnMobile = fnRow
    End Sub

    Public Sub showLandline(ByVal fnRow As Integer)
        If p_oTrans.Detail.applicant_info.landline.Count = 0 Then Exit Sub
        With p_oTrans.Detail.applicant_info
            txtPerso14.Text = .landline(fnRow).sPhoneNox
        End With
        pnLandline = fnRow
    End Sub

    Public Sub showEmail(ByVal fnRow As Integer)
        If p_oTrans.Detail.applicant_info.email_address.Count = 0 Then Exit Sub
        With p_oTrans.Detail.applicant_info
            txtPerso17.Text = .email_address(fnRow).sEmailAdd
        End With
        pnEmail = fnRow
    End Sub

    Public Sub showSpouseMobile(ByVal fnRow As Integer)
        If p_oTrans.Detail.spouse_info.personal_info.mobile_number.Count = 0 Then Exit Sub
        With p_oTrans.Detail.spouse_info.personal_info
            txtSpoIn08.Text = .mobile_number(fnRow).sMobileNo
            chk04.Checked = IIf(.mobile_number(fnRow).cPostPaid <> "1", False, True)
        End With
        pnSMobile = fnRow
    End Sub

    Public Sub showComak(ByVal fnRow As Integer)
        If p_oTrans.Detail.comaker_info.mobile_number.Count = 0 Then Exit Sub
        With p_oTrans.Detail.comaker_info
            txtCoMak09.Text = .mobile_number(fnRow).sMobileNo
            chk03.Checked = IIf(.mobile_number(fnRow).cPostPaid <> "1", False, True)
        End With
        pnComak = fnRow
    End Sub

    Public Sub showSpouseLandline(ByVal fnRow As Integer)
        If p_oTrans.Detail.spouse_info.personal_info.landline.Count = 0 Then Exit Sub
        With p_oTrans.Detail.spouse_info.personal_info
            txtSpoIn11.Text = .landline(fnRow).sPhoneNox
        End With
        pnSLandLine = fnRow
    End Sub

    Public Sub showSpouseEmail(ByVal fnRow As Integer)
        If p_oTrans.Detail.spouse_info.personal_info.email_address.Count = 0 Then Exit Sub
        With p_oTrans.Detail.spouse_info.personal_info
            txtSpoIn15.Text = .email_address(fnRow).sEmailAdd
        End With
        pSnEmail = fnRow
    End Sub

    Private Sub clearDependent()
        With p_oTrans.Detail.disbursement_info.dependent_info
            cmb50.SelectedIndex = -1
            cmb51.SelectedIndex = -1
            cmb52.SelectedIndex = -1
            cmb53.SelectedIndex = -1
            cmb63.SelectedIndex = -1
            cmb64.SelectedIndex = -1
            cmb65.SelectedIndex = -1
            cmb66.SelectedIndex = -1
            cmb67.SelectedIndex = -1
            cmb68.SelectedIndex = -1
            cmb69.SelectedIndex = -1
        End With
    End Sub

    Private Sub txtOther_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.other_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtOther" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .personal_reference(pnReference).sRefrNmex = loTxt.Text
                        Call loadReference_Info()
                    Case 1
                        .personal_reference(pnReference).sRefrMPNx = loTxt.Text
                        Call loadReference_Info()
                    Case 2
                        .personal_reference(pnReference).sRefrAddx = loTxt.Text
                        Call loadReference_Info()
                    Case 4
                        .sSrceInfo = loTxt.Text
                End Select
            End If
        End With
    End Sub

    Private Function DataComplete() As Boolean
        With p_oTrans.Detail
            If .applicant_info.sLastName = "" Then
                MessageBox.Show("No Applicant LastName entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtPerso00.Focus()
                Return False
            ElseIf .applicant_info.sFrstName = "" Then
                MessageBox.Show("No Applicant Firstname entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtPerso01.Focus()
                Return False
            ElseIf .applicant_info.sMiddName = "" Then
                MessageBox.Show("No Applicant Middlename entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtPerso02.Focus()
                Return False
            ElseIf Not IsDate(.applicant_info.dBirthDte) Then
                MessageBox.Show("Invalid Birth Date entry detected", "No entry",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtPerso05.Focus()
                Return False
            ElseIf .applicant_info.sBirthPlc = "" Then
                MessageBox.Show("No Birth Place entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtPerso06.Focus()
                Return False
            ElseIf .applicant_info.sCitizenx = "" Then
                MessageBox.Show("No Citizenship entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtPerso07.Focus()
                Return False
            ElseIf .applicant_info.cCvilStat = "" Then
                MessageBox.Show("No Civil Status entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                cmb03.Focus()
                Return False
            ElseIf .applicant_info.cGenderCd = "" Then
                MessageBox.Show("No Gender entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                cmb04.Focus()
                Return False
            ElseIf .residence_info.present_address.sAddress1 = "" And .residence_info.present_address.sAddress2 = "" Then
                MessageBox.Show("No present Phase #/Lot #/Sitio or Street name entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 2
                txtResid03.Focus()
                Return False
            ElseIf .residence_info.present_address.sTownIDxx = "" Then
                MessageBox.Show("No present Town/City entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 2
                txtResid05.Focus()
                Return False
            ElseIf .residence_info.present_address.sBrgyIDxx = "" Then
                MessageBox.Show("No present barangay entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 2
                txtResid06.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sAddress1 = "" And .residence_info.permanent_address.sAddress2 = "" Then
                MessageBox.Show("No permanent Phase #/Lot #/Sitio or Street name entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 2
                txtResid14.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sTownIDxx = "" Then
                MessageBox.Show("No permanent Town/City entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 2
                txtResid16.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sBrgyIDxx = "" Then
                MessageBox.Show("No permanent barangay entry detected", "No entry",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 2
                txtResid17.Focus()
                Return False
            ElseIf .applicant_info.mobile_number.Count > 0 Then
                For lnCtr As Integer = 0 To .applicant_info.mobile_number.Count - 1
                    Select Case lnCtr
                        Case 0
                            If .applicant_info.mobile_number(lnCtr).sMobileNo = "" Then
                                MessageBox.Show("No mobile number entry detected...", "No entry",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tabControl00.SelectedIndex = 1
                                txtPerso08.Focus()
                                Return False
                                Exit For
                            End If
                    End Select
                Next
            End If

            If .applicant_info.facebook.sFBAcctxx = "" Then
                .applicant_info.facebook.cAcctStat = ""
                cmb62.SelectedIndex = -1
                .applicant_info.facebook.nNoFriend = 0
                txtPerso24.Text = 0
                .applicant_info.facebook.nYearxxxx = 0
                txtPerso25.Text = 0
            End If

            If .applicant_info.cCvilStat = "1" Or .applicant_info.cCvilStat = "5" Then
                    If .spouse_info.personal_info.sLastName = "" Then
                        MessageBox.Show("No Spouse Lastname entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn00.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sFrstName = "" Then
                        MessageBox.Show("No Spouse Firstname entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn01.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sMiddName = "" Then
                        MessageBox.Show("No Spouse Middlename entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn03.Focus()
                        Return False
                    ElseIf Not IsDate(.spouse_info.personal_info.dBirthDte) Then
                        MessageBox.Show("Invalid Spouse Birth date entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn05.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sBirthPlc = "" Then
                        MessageBox.Show("No Spouse Birth Place entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn06.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sCitizenx = "" Then
                        MessageBox.Show("No Spouse Citizenship entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn07.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.cCvilStat = "" Then
                        MessageBox.Show("No Spouse Civil Status entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        cmb33.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.cGenderCd = "" Then
                        MessageBox.Show("No Spouse Gender entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 8
                        cmb34.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.present_address.sAddress1 = "" And .spouse_info.residence_info.present_address.sAddress2 = "" Then
                        MessageBox.Show("No Spouse Present Phase #/Lot #/Sitio or Street Name entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe02.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.present_address.sTownIDxx = "" Then
                        MessageBox.Show("No Spouse present Town/City entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe04.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.present_address.sBrgyIDxx = "" Then
                        MessageBox.Show("No Spouse present barangay entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe05.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.permanent_address.sAddress1 = "" And .spouse_info.residence_info.permanent_address.sAddress2 = "" Then
                        MessageBox.Show("No Spouse Permanent Phase #/Lot #/Sitio or Street Name entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe11.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.permanent_address.sTownIDxx = "" Then
                        MessageBox.Show("No Spouse permanent Town/City entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe13.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.permanent_address.sBrgyIDxx = "" Then
                        MessageBox.Show("No Spouse permanent barangay entry detected", "No entry",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe14.Focus()
                        Return False
                    End If
                ElseIf .spouse_info.personal_info.facebook.sFBAcctxx = "" Then
                    .spouse_info.personal_info.facebook.cAcctStat = ""
                    cmb75.SelectedIndex = -1
                    .spouse_info.personal_info.facebook.nNoFriend = 0
                    txtSpoIn21.Text = 0
                    .spouse_info.personal_info.facebook.nYearxxxx = 0
                    txtSpoIn21.Text = 0
                End If
        End With
        Return True
    End Function

    Private Sub cmb_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim loChk As ComboBox
        loChk = CType(sender, System.Windows.Forms.ComboBox)
        If e.KeyCode = Keys.Back Then

            Dim lnIndex As Integer
            With p_oTrans.Detail
                lnIndex = Val(Mid(loChk.Name, 4))
                Select Case lnIndex
                    Case 0
                        .cUnitAppl = ""
                        loChk.SelectedIndex = -1
                    Case 1
                        .cApplType = ""
                        loChk.SelectedIndex = -1
                    Case 3
                        .applicant_info.cCvilStat = ""
                        loChk.SelectedIndex = -1
                    Case 4
                        .applicant_info.cGenderCd = ""
                        loChk.SelectedIndex = -1
                    Case 62
                        .applicant_info.facebook.cAcctStat = ""
                        loChk.SelectedIndex = -1
                    Case 90
                        .residence_info.cOwnershp = ""
                        loChk.SelectedIndex = -1
                    Case 5
                        .residence_info.cOwnOther = ""
                        loChk.SelectedIndex = -1
                    Case 91
                        .residence_info.rent_others.cRntOther = ""
                        loChk.SelectedIndex = -1
                    Case 7
                        .residence_info.cHouseTyp = ""
                        loChk.SelectedIndex = -1
                    Case 6
                        .residence_info.cGaragexx = ""
                        loChk.SelectedIndex = -1
                    Case 8
                        .means_info.cIncmeSrc = ""
                        loChk.SelectedIndex = -1
                    Case 9
                        .means_info.employed.cEmpSectr = ""
                        loChk.SelectedIndex = -1
                    Case 10
                        .means_info.employed.cUniforme = ""
                        loChk.SelectedIndex = -1
                    Case 11
                        .means_info.employed.cMilitary = ""
                        loChk.SelectedIndex = -1
                    Case 12
                        .means_info.employed.cGovtLevl = ""
                        loChk.SelectedIndex = -1
                    Case 13
                        .means_info.employed.cCompLevl = ""
                        loChk.SelectedIndex = -1
                    Case 14
                        .means_info.employed.cEmpLevlx = ""
                        loChk.SelectedIndex = -1
                    Case 15
                        .means_info.employed.cOcCatgry = ""
                        loChk.SelectedIndex = -1
                    Case 16
                        .means_info.employed.cOFWRegnx = ""
                        loChk.SelectedIndex = -1
                    Case 17
                        .means_info.self_employed.cBusTypex = ""
                        loChk.SelectedIndex = -1
                    Case 18
                        .means_info.self_employed.cOwnTypex = ""
                        loChk.SelectedIndex = -1
                    Case 19
                        .disbursement_info.properties.with3Whls_info.cWithWhls = ""
                        loChk.SelectedIndex = -1
                    Case 20
                        .disbursement_info.properties.with2Whls_info.cWithWhls = ""
                        loChk.SelectedIndex = -1
                    Case 24
                        .disbursement_info.bank_account.sAcctType = ""
                        loChk.SelectedIndex = -1
                    Case 26
                        .other_info.sUnitUser = ""
                        loChk.SelectedIndex = -1
                    Case 27
                        .other_info.sUsr2Buyr = ""
                        loChk.SelectedIndex = -1
                    Case 28
                        .other_info.sPurposex = ""
                        loChk.SelectedIndex = -1
                    Case 29
                        .other_info.sUnitPayr = ""
                        loChk.SelectedIndex = -1
                    Case 30
                        .other_info.sPyr2Buyr = ""
                        loChk.SelectedIndex = -1
                    Case 33
                        .spouse_info.personal_info.cCvilStat = ""
                        loChk.SelectedIndex = -1
                    Case 34
                        .spouse_info.personal_info.cGenderCd = ""
                        loChk.SelectedIndex = -1
                    Case 35
                        .spouse_means.cIncmeSrc = ""
                        loChk.SelectedIndex = -1
                    Case 36
                        .spouse_means.employed.cEmpSectr = ""
                        loChk.SelectedIndex = -1
                    Case 37
                        .spouse_means.employed.cUniforme = ""
                        loChk.SelectedIndex = -1
                    Case 38
                        .spouse_means.employed.cMilitary = ""
                        loChk.SelectedIndex = -1
                    Case 39
                        .spouse_means.employed.cGovtLevl = ""
                        loChk.SelectedIndex = -1
                    Case 40
                        .spouse_means.employed.cCompLevl = ""
                        loChk.SelectedIndex = -1
                    Case 41
                        .spouse_means.employed.cEmpLevlx = ""
                        loChk.SelectedIndex = -1
                    Case 42
                        .spouse_means.employed.cOcCatgry = ""
                        loChk.SelectedIndex = -1
                    Case 43
                        .spouse_means.employed.cOFWRegnx = ""
                        loChk.SelectedIndex = -1
                    Case 60
                        .disbursement_info.properties.with4Whls_info.cWithWhls = ""
                        loChk.SelectedIndex = -1
                    Case 63
                        .disbursement_info.dependent_info.children(pnChild).sRelatnCD = ""
                        loChk.SelectedIndex = -1
                    Case 64
                        .disbursement_info.dependent_info.children(pnChild).cIsPupilx = ""
                        loChk.SelectedIndex = -1
                    Case 65
                        .disbursement_info.dependent_info.children(pnChild).cIsPrivte = ""
                        loChk.SelectedIndex = -1
                    Case 66
                        .disbursement_info.dependent_info.children(pnChild).sEducLevl = ""
                        loChk.SelectedIndex = -1
                    Case 67
                        .disbursement_info.dependent_info.children(pnChild).cIsSchlrx = ""
                        loChk.SelectedIndex = -1
                    Case 68
                        .disbursement_info.dependent_info.children(pnChild).cHasWorkx = ""
                        loChk.SelectedIndex = -1
                    Case 69
                        .disbursement_info.dependent_info.children(pnChild).cWorkType = ""
                        loChk.SelectedIndex = -1
                    Case 50
                        .disbursement_info.dependent_info.children(pnChild).cHouseHld = ""
                        loChk.SelectedIndex = -1
                    Case 51
                        .disbursement_info.dependent_info.children(pnChild).cDependnt = ""
                        loChk.SelectedIndex = -1
                    Case 52
                        .disbursement_info.dependent_info.children(pnChild).cIsChildx = ""
                        loChk.SelectedIndex = -1
                    Case 53
                        .disbursement_info.dependent_info.children(pnChild).cIsMarrdx = ""
                        loChk.SelectedIndex = -1
                    Case 84
                        .spouse_info.residence_info.rent_others.cRntOther = ""
                        loChk.SelectedIndex = -1
                    Case 80
                        .spouse_info.residence_info.cOwnershp = ""
                        loChk.SelectedIndex = -1
                    Case 81
                        .spouse_info.residence_info.cOwnOther = ""
                        loChk.SelectedIndex = -1
                    Case 82
                        .spouse_info.residence_info.cGaragexx = ""
                        loChk.SelectedIndex = -1
                    Case 83
                        .spouse_info.residence_info.cHouseTyp = ""
                        loChk.SelectedIndex = -1
                    Case 97
                        .spouse_means.employed.cEmpStatx = ""
                        loChk.SelectedIndex = -1
                    Case 98
                        .spouse_means.self_employed.cBusTypex = ""
                        loChk.SelectedIndex = -1
                    Case 99
                        .spouse_means.self_employed.cOwnTypex = ""
                        loChk.SelectedIndex = -1
                    Case 89
                        .spouse_means.self_employed.cOwnSizex = ""
                        loChk.SelectedIndex = -1
                    Case 70
                        .comaker_info.cIncmeSrc = ""
                        loChk.SelectedIndex = -1
                    Case 71
                        .comaker_info.sReltnCde = ""
                        loChk.SelectedIndex = -1
                    Case 96
                        .means_info.employed.cEmpStatx = ""
                        loChk.SelectedIndex = -1
                    Case 95
                        .means_info.self_employed.cOwnSizex = ""
                        loChk.SelectedIndex = -1
                    Case 85
                        .means_info.financed.sReltnCde = ""
                        loChk.SelectedIndex = -1
                    Case 86
                        .means_info.pensioner.cPenTypex = ""
                        loChk.SelectedIndex = -1
                    Case 75
                        .spouse_info.personal_info.facebook.cAcctStat = ""
                        loChk.SelectedIndex = -1
                    Case 100
                        .disbursement_info.properties.with4Whls_info.cOwnerShp = ""
                        loChk.SelectedIndex = -1
                    Case 101
                        .disbursement_info.properties.with4Whls_info.cTermxxxx = ""
                        loChk.SelectedIndex = -1
                    Case 102
                        .disbursement_info.properties.with4Whls_info.cStatusxx = ""
                        loChk.SelectedIndex = -1
                    Case 103
                        .disbursement_info.properties.with3Whls_info.cOwnerShp = ""
                        loChk.SelectedIndex = -1
                    Case 104
                        .disbursement_info.properties.with3Whls_info.cTermxxxx = ""
                        loChk.SelectedIndex = -1
                    Case 105
                        .disbursement_info.properties.with3Whls_info.cStatusxx = ""
                        loChk.SelectedIndex = -1
                    Case 106
                        .disbursement_info.properties.with2Whls_info.cOwnerShp = ""
                        loChk.SelectedIndex = -1
                    Case 107
                        .disbursement_info.properties.with2Whls_info.cTermxxxx = ""
                        loChk.SelectedIndex = -1
                    Case 108
                        .disbursement_info.properties.with2Whls_info.cStatusxx = ""
                        loChk.SelectedIndex = -1
                End Select
            End With
        End If
    End Sub

    Private Sub ClearSpouseInfo(ByVal loControl As Control)
        Dim loTxt As Control
        Dim loIndex As Integer
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call ClearSpouseInfo(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoIn" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoRe" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Then
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
        Next
    End Sub

    Private Sub clearSpouseDetails()
        With p_oTrans.Detail
            .spouse_info.personal_info.cCvilStat = ""
            .spouse_info.personal_info.cGenderCd = ""
            .spouse_info.personal_info.sLastName = ""
            .spouse_info.personal_info.sFrstName = ""
            .spouse_info.personal_info.sSuffixNm = ""
            .spouse_info.personal_info.sMiddName = ""
            .spouse_info.personal_info.sNickName = ""
            .spouse_info.personal_info.sCitizenx = ""
            .spouse_info.personal_info.sBirthPlc = ""
            .spouse_info.personal_info.dBirthDte = ""
            .spouse_info.personal_info.sMaidenNm = ""
            .spouse_info.personal_info.mobile_number.Clear()
            .spouse_info.personal_info.landline.Clear()
            .spouse_info.personal_info.email_address.Clear()

            .spouse_info.personal_info.facebook.cAcctStat = ""
            .spouse_info.personal_info.facebook.sFBAcctxx = ""
            .spouse_info.personal_info.facebook.nNoFriend = 0
            .spouse_info.personal_info.facebook.nYearxxxx = 0
            .spouse_info.personal_info.sVibeAcct = ""

            .spouse_info.residence_info.present_address.sTownIDxx = ""
            .spouse_info.residence_info.present_address.sBrgyIDxx = ""
            .spouse_info.residence_info.present_address.sLandMark = ""
            .spouse_info.residence_info.present_address.sHouseNox = ""
            .spouse_info.residence_info.present_address.sAddress1 = ""
            .spouse_info.residence_info.present_address.sAddress2 = ""
            .spouse_info.residence_info.rent_others.cRntOther = ""
            .spouse_info.residence_info.rent_others.nLenStayx = 0
            .spouse_info.residence_info.rent_others.nRentExps = 0
            .spouse_info.residence_info.cOwnershp = ""
            .spouse_info.residence_info.cOwnOther = ""
            .spouse_info.residence_info.cGaragexx = ""
            .spouse_info.residence_info.cHouseTyp = ""
            .spouse_info.residence_info.permanent_address.sTownIDxx = ""
            .spouse_info.residence_info.permanent_address.sBrgyIDxx = ""
            .spouse_info.residence_info.permanent_address.sLandMark = ""
            .spouse_info.residence_info.permanent_address.sHouseNox = ""
            .spouse_info.residence_info.permanent_address.sAddress1 = ""
            .spouse_info.residence_info.permanent_address.sAddress2 = ""
            .spouse_info.residence_info.sCtkReltn = ""

            .spouse_means.cIncmeSrc = ""
            .spouse_means.employed.cEmpSectr = ""
            .spouse_means.employed.cUniforme = ""
            .spouse_means.employed.cMilitary = ""
            .spouse_means.employed.cGovtLevl = ""
            .spouse_means.employed.cCompLevl = ""
            .spouse_means.employed.cEmpLevlx = ""
            .spouse_means.employed.cOcCatgry = ""
            .spouse_means.employed.cOFWRegnx = ""
            .spouse_means.employed.cEmpStatx = ""
            .spouse_means.self_employed.cBusTypex = ""
            .spouse_means.self_employed.cOwnTypex = ""
            .spouse_means.self_employed.cOwnSizex = ""
            .spouse_means.employed.sIndstWrk = ""
            .spouse_means.employed.sEmployer = ""
            .spouse_means.employed.sWrkAddrx = ""
            .spouse_means.employed.sFunction = ""
            .spouse_means.employed.nLenServc = 0
            .spouse_means.employed.nSalaryxx = 0
            .spouse_means.employed.sWrkTelno = ""
            .spouse_means.self_employed.sIndstBus = ""
            .spouse_means.self_employed.sBusiness = ""
            .spouse_means.self_employed.sBusAddrx = ""
            .spouse_means.self_employed.nBusLenxx = ""
            .spouse_means.self_employed.nBusIncom = 0
            .spouse_means.self_employed.nMonExpns = 0
            .spouse_means.employed.sOFWNatnx = ""
            .spouse_means.employed.sWrkTownx = ""
            .spouse_means.employed.sPosition = ""
            .spouse_means.self_employed.sBusTownx = ""
            Call loadSpouseMobile()
            Call loadSpouseLandline()
            Call loadSpouseEmail()
        End With
    End Sub


    Public Function isWithSpouse(ByVal groupbox As GroupBox) As Boolean
        Dim loTxt As Control
        For Each loTxt In groupbox.Controls
            If TypeOf loTxt Is TextBox Then
                If loTxt.Text <> "" Then
                    Dim ans As String
                    ans = MsgBox("Selected customer's civil status doesnt require spouse information!" & vbCrLf & _
                                 "By proceeding all info of spouse will be remove...", vbCritical + vbYesNo, "Confirm")
                    If ans = vbYes Then
                        ClearSpouseInfo(grpBox09)
                        ClearSpouseInfo(grpBox10)
                        ClearSpouseInfo(grpBox11)
                        clearSpouseDetails()
                        Return True
                        Exit Function
                    Else
                        Return False
                        Exit Function
                    End If
                End If
            End If
        Next

        Return True
    End Function

    Private Sub txtResid_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.residence_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtResid" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 1
                        .present_address.sLandMark = loTxt.Text
                    Case 2
                        .present_address.sHouseNox = loTxt.Text
                    Case 3
                        .present_address.sAddress1 = loTxt.Text
                    Case 4
                        .present_address.sAddress2 = loTxt.Text
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .rent_others.nLenStayx = CInt(loTxt.Text)
                    Case 10
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .rent_others.nRentExps = CDbl(loTxt.Text)
                    Case 11
                        .sCtkReltn = loTxt.Text
                    Case 12
                        .permanent_address.sLandMark = loTxt.Text
                    Case 13
                        .permanent_address.sHouseNox = loTxt.Text
                    Case 14
                        .permanent_address.sAddress1 = loTxt.Text
                    Case 15
                        .permanent_address.sAddress2 = loTxt.Text
                End Select
            End If
        End With
    End Sub
    Private Sub txtDisbu_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.disbursement_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtDisbu" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(loTxt.Text, 2)
                        End If
                        .monthly_expenses.nElctrcBl = CDbl(loTxt.Text)
                    Case 1
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .monthly_expenses.nWaterBil = CDbl(loTxt.Text)
                    Case 2
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .monthly_expenses.nFoodAllw = CDbl(loTxt.Text)
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .monthly_expenses.nLoanAmtx = CDbl(loTxt.Text)
                    Case 4
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .monthly_expenses.nEductnxx = CDbl(loTxt.Text)
                    Case 7
                        .bank_account.sBankName = loTxt.Text
                    Case 8
                        .credit_card.sBankName = loTxt.Text
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(0, 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .credit_card.nCrdLimit = CDbl(loTxt.Text)
                    Case 10
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .credit_card.nSinceYrx = CInt(loTxt.Text)
                    Case 11
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .dependent_info.nHouseHld = CInt(loTxt.Text)
                    Case 12 To 15, 17
                        .dependent_info.children(pnChild).sFullName = txtDisbu12.Text
                        .dependent_info.children(pnChild).nDepdAgex = txtDisbu13.Text
                        .dependent_info.children(pnChild).sSchlName = txtDisbu14.Text
                        .dependent_info.children(pnChild).sSchlAddr = txtDisbu15.Text
                        .dependent_info.children(pnChild).sCompanyx = txtDisbu17.Text
                        Call loadDependent_Info()
                End Select
            End If
        End With
    End Sub

    Private Sub txtSpoRe_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Detail.spouse_info.residence_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtSpoRe" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .present_address.sLandMark = loTxt.Text
                    Case 1
                        .present_address.sHouseNox = loTxt.Text
                    Case 2
                        .present_address.sAddress1 = loTxt.Text
                    Case 3
                        .present_address.sAddress2 = loTxt.Text
                    Case 6
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .rent_others.nLenStayx = CInt(loTxt.Text)
                    Case 7
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .rent_others.nRentExps = CDbl(loTxt.Text)
                    Case 8
                        .sCtkReltn = loTxt.Text
                    Case 9
                        .permanent_address.sLandMark = loTxt.Text
                    Case 10
                        .permanent_address.sHouseNox = loTxt.Text
                    Case 11
                        .permanent_address.sAddress1 = loTxt.Text
                    Case 12
                        .permanent_address.sAddress2 = loTxt.Text
                End Select
            End If
        End With
    End Sub

    Private Sub dgv00_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv00.Click
        Dim pnRow As Integer
        pnRow = dgv00.CurrentRow.Index
        Call showDependentInfo(pnRow)
    End Sub

    Private Sub dgv01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv01.Click
        Dim pnRow As Integer
        pnRow = dgv01.CurrentRow.Index
        Call showReference(pnRow)
    End Sub

    Private Sub dgv02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv02.Click
        Dim pnRow As Integer
        pnRow = dgv02.CurrentRow.Index
        Call showAppliMobile(pnRow)
    End Sub

    Private Sub dgv03_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv03.Click
        Dim pnRow As Integer
        pnRow = dgv03.CurrentRow.Index
        Call showLandline(pnRow)
    End Sub

    Private Sub dgv04_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv04.Click
        Dim pnRow As Integer
        pnRow = dgv04.CurrentRow.Index
        Call showEmail(pnRow)
    End Sub

    Private Sub dgv05_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv05.Click
        Dim pnRow As Integer
        pnRow = dgv05.CurrentRow.Index
        Call showSpouseMobile(pnRow)
    End Sub

    Private Sub dgv06_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv06.Click
        Dim pnRow As Integer
        pnRow = dgv06.CurrentRow.Index
        Call showSpouseLandline(pnRow)
    End Sub

    Private Sub dgv07_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv07.Click
        Dim pnRow As Integer
        pnRow = dgv07.CurrentRow.Index
        Call showSpouseEmail(pnRow)
    End Sub

    Private Sub dgv08_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv08.Click
        Dim pnRow As Integer
        pnRow = dgv08.CurrentRow.Index
        Call showComak(pnRow)
    End Sub

    Private Sub chk01_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk01.CheckStateChanged
        With p_oTrans.Detail.residence_info
            If chk01.Checked = True Then
                txtResid12.Text = .present_address.sLandMark
                .permanent_address.sLandMark = .present_address.sLandMark
                txtResid13.Text = .present_address.sHouseNox
                .permanent_address.sHouseNox = .present_address.sHouseNox
                txtResid14.Text = .present_address.sAddress1
                .permanent_address.sAddress1 = .present_address.sAddress1
                txtResid15.Text = .present_address.sAddress2
                .permanent_address.sAddress2 = .present_address.sAddress2
                txtResid16.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                .permanent_address.sTownIDxx = .present_address.sTownIDxx
                txtResid17.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                .permanent_address.sBrgyIDxx = .present_address.sBrgyIDxx
            Else
                txtResid12.Text = ""
                .permanent_address.sLandMark = ""
                txtResid13.Text = ""
                .permanent_address.sHouseNox = ""
                txtResid14.Text = ""
                .permanent_address.sAddress1 = ""
                txtResid15.Text = ""
                .permanent_address.sAddress2 = ""
                txtResid16.Text = ""
                .permanent_address.sTownIDxx = ""
                txtResid17.Text = ""
                .permanent_address.sBrgyIDxx = ""
            End If
        End With
    End Sub

    Private Sub chk02_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk02.CheckStateChanged
        With p_oTrans.Detail.spouse_info.residence_info
            If chk02.Checked = True Then
                txtSpoRe09.Text = .present_address.sLandMark
                .permanent_address.sLandMark = .present_address.sLandMark
                txtSpoRe10.Text = .present_address.sHouseNox
                .permanent_address.sHouseNox = .present_address.sHouseNox
                txtSpoRe11.Text = .present_address.sAddress1
                .permanent_address.sAddress1 = .present_address.sAddress1
                txtSpoRe12.Text = .present_address.sAddress2
                .permanent_address.sAddress2 = .present_address.sAddress2
                txtSpoRe13.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                .permanent_address.sTownIDxx = .present_address.sTownIDxx
                txtSpoRe14.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                .permanent_address.sBrgyIDxx = .present_address.sBrgyIDxx
            Else
                txtSpoRe09.Text = ""
                .permanent_address.sLandMark = ""
                txtSpoRe10.Text = ""
                .permanent_address.sHouseNox = ""
                txtSpoRe11.Text = ""
                .permanent_address.sAddress1 = ""
                txtSpoRe12.Text = ""
                .permanent_address.sAddress2 = ""
                txtSpoRe13.Text = ""
                .permanent_address.sTownIDxx = ""
                txtSpoRe14.Text = ""
                .permanent_address.sBrgyIDxx = ""
            End If
        End With
    End Sub

    Private Sub chk00_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk00.CheckStateChanged
        With p_oTrans.Detail.applicant_info
            If chk00.Checked = True Then
                .mobile_number(pnMobile).cPostPaid = 1
            Else
                .mobile_number(pnMobile).cPostPaid = 0
            End If
            Call loadAppliMobile()
        End With
    End Sub

    Private Sub chk03_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk03.CheckStateChanged
        With p_oTrans.Detail.comaker_info
            If chk03.Checked = True Then
                .mobile_number(pnComak).cPostPaid = 1
            Else
                .mobile_number(pnComak).cPostPaid = 0
            End If
            Call loadComakMobile()
        End With
    End Sub

    Private Sub chk04_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk04.CheckStateChanged
        With p_oTrans.Detail.spouse_info.personal_info
            If chk04.Checked = True Then
                .mobile_number(pnSMobile).cPostPaid = 1
            Else
                .mobile_number(pnSMobile).cPostPaid = 0
            End If
            Call loadSpouseMobile()
        End With
    End Sub
End Class
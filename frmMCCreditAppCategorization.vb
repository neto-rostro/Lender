Option Explicit On
Imports ggcGOCAS
Imports ggcAppDriver
Imports ggcGOCAS.GOCASCI
Imports Newtonsoft.Json

Public Class frmMCCreditAppCategorization
    Public p_oValidate As GOCASCI
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private pnRow As Integer
    Private pdRow As Integer
    Private psButton As Integer
    Private psMobile As String
    Dim loFrm As frmMCCreditAppResult
    Dim loFrms As FrmCITagging
    Dim loFrm1 As frmCITaggingViewing

    Private WithEvents p_oTrans As ggcGOCAS.GOCASApplication
    Dim psTransNox As String

    Public WriteOnly Property sTransNox
        Set(ByVal value)
            psTransNox = value
        End Set
    End Property

    Private Sub frmMCCreditAppCategorization_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMCCreditAppCategorization_Activated")
        If pnLoadx = 1 Then
            initGrid(dgv00)
            initReference(dgv01)
            Call p_oTrans.NewTransaction()
            ClearFields(Me.Panel1)
            ClearFields(Me.Panel2)
            initDisplay()
            initButton(0)
            If psTransNox <> "" Then
                ClearFields(Me.Panel1)
                ClearFields(Me.Panel2)
                If p_oTrans.OpenTransaction(psTransNox) Then
                    Call loadTransaction()
                End If
            End If
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmMCCreditAppCategorization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmMCCreditAppCategorization_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcGOCAS.GOCASApplication(p_oAppDriver, 30)

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
            Call grpCancelHandler(Me, GetType(TextBox), "txtCoAdd", "Validating", AddressOf txtComakRes_Validating)

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
            Call grpEventHandler(Me, GetType(TextBox), "txtCoAdd", "GotFocus", AddressOf txtField_GotFocus)

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
            Call grpKeyHandler(Me, GetType(TextBox), "txtCoAdd", "KeyDown", AddressOf txtField_KeyDown)

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
            Call grpEventHandler(Me, GetType(TextBox), "txtCoAdd", "LostFocus", AddressOf txtField_LostFocus)

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
            Call grpKeyHandler(Me, GetType(TextBox), "txtCoAdd", "KeyDown", AddressOf ArrowKeys_Keydown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            Call grpKeyHandler(Me, GetType(ComboBox), "cmb", "KeyDown", AddressOf cmb_KeyDown)
            Call grpEventHandler(Me, GetType(ComboBox), "cmb", "SelectedIndexChanged", AddressOf combobox_SelectedIndexChanged)

            pnLoadx = 1
        End If
    End Sub

    Private Sub isEntryOk()
        With p_oTrans.Category
            p_oTrans.Description.cUnitAppl = IIf(.cUnitAppl <> cmb00.Tag, "0", "1")
            p_oTrans.Description.cApplType = IIf(.cApplType <> cmb01.Tag, "0", "1")
            p_oTrans.Description.nDownPaym = IIf(.nDownPaym <> Double.Parse(txtIntro01.Tag), "0", "1")
            p_oTrans.Description.nAcctTerm = IIf(.nAcctTerm <> txtIntro02.Tag, "0", "1")
            p_oTrans.Description.nMonAmort = IIf(.nMonAmort <> Double.Parse(txtIntro03.Tag), "0", "1")
            p_oTrans.Description.sUnitAppl = IIf(.sUnitAppl <> txtIntro04.Tag, "0", "1")
            p_oTrans.Description.dTargetDt = IIf(.dTargetDt <> txtIntro06.Tag, "0", "1")
            p_oTrans.Description.sModelIDx = IIf(.sModelIDx <> txtIntro00.Tag, "0", "1")
            p_oTrans.Description.sBranchCd = IIf(.sBranchCd <> txtIntro05.Tag, "0", "1")

            p_oTrans.Description.applicant_info.sLastName = IIf(.applicant_info.sLastName <> txtPerso00.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sFrstName = IIf(.applicant_info.sFrstName <> txtPerso01.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sMiddName = IIf(.applicant_info.sMiddName <> txtPerso02.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sSuffixNm = IIf(.applicant_info.sSuffixNm <> txtPerso03.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sNickName = IIf(.applicant_info.sNickName <> txtPerso04.Tag, "0", "1")

            If Not IsDate(txtPerso05.Tag) Then
                p_oTrans.Description.applicant_info.dBirthDte = IIf(.applicant_info.dBirthDte <> txtPerso05.Tag, "0", "1")
            Else
                p_oTrans.Description.applicant_info.dBirthDte = IIf(.applicant_info.dBirthDte <> Date.Parse(txtPerso05.Tag), "0", "1")
            End If

            p_oTrans.Description.applicant_info.sBirthPlc = IIf(.applicant_info.sBirthPlc <> txtPerso06.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sCitizenx = IIf(.applicant_info.sCitizenx <> txtPerso07.Tag, "0", "1")

            p_oTrans.Description.applicant_info.cCvilStat = IIf(.applicant_info.cCvilStat <> cmb03.Tag, "0", "1")
            p_oTrans.Description.applicant_info.cGenderCd = IIf(.applicant_info.cGenderCd <> cmb04.Tag, "0", "1")
            p_oTrans.Description.applicant_info.facebook.cAcctStat = IIf(.applicant_info.facebook.cAcctStat <> cmb62.Tag, "0", "1")
            p_oTrans.Description.applicant_info.facebook.sFBAcctxx = IIf(.applicant_info.facebook.sFBAcctxx <> txtPerso20.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sVibeAcct = IIf(.applicant_info.sVibeAcct <> txtPerso21.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sMaidenNm = IIf(.applicant_info.sMaidenNm <> txtPerso22.Tag, "0", "1")
            p_oTrans.Description.applicant_info.facebook.nNoFriend = IIf(.applicant_info.facebook.nNoFriend <> Integer.Parse(txtPerso24.Tag), "0", "1")
            p_oTrans.Description.applicant_info.facebook.nYearxxxx = IIf(.applicant_info.facebook.nYearxxxx <> Integer.Parse(txtPerso25.Tag), "0", "1")

            For nCtr As Integer = 0 To .applicant_info.mobile_number.Count - 1
                p_oTrans.Description.applicant_info.mobile_number.Add(New GOCASConst.mobileno_param)
                Select Case nCtr
                    Case 0
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).sMobileNo = IIf(.applicant_info.mobile_number(nCtr).sMobileNo <> txtPerso08.Tag, "0", "1")
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).cPostPaid = IIf(.applicant_info.mobile_number(nCtr).cPostPaid <> chk00.Tag, "0", "1")
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).nPostYear = IIf(.applicant_info.mobile_number(nCtr).nPostYear <> txtPerso11.Tag, "0", "1")
                    Case 1
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).sMobileNo = IIf(.applicant_info.mobile_number(nCtr).sMobileNo <> txtPerso09.Tag, "0", "1")
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).cPostPaid = IIf(.applicant_info.mobile_number(nCtr).cPostPaid <> chk01.Tag, "0", "1")
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).nPostYear = IIf(.applicant_info.mobile_number(nCtr).nPostYear <> txtPerso12.Tag, "0", "1")
                    Case 2
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).sMobileNo = IIf(.applicant_info.mobile_number(nCtr).sMobileNo <> txtPerso10.Tag, "0", "1")
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).cPostPaid = IIf(.applicant_info.mobile_number(nCtr).cPostPaid <> chk02.Tag, "0", "1")
                        p_oTrans.Description.applicant_info.mobile_number(nCtr).nPostYear = IIf(.applicant_info.mobile_number(nCtr).nPostYear <> txtPerso13.Tag, "0", "1")
                End Select
            Next nCtr

            For nCtr As Integer = 0 To .applicant_info.landline.Count - 1
                p_oTrans.Description.applicant_info.landline.Add(New GOCASConst.landline_param)
                Select Case nCtr
                    Case 0
                        p_oTrans.Description.applicant_info.landline(nCtr).sPhoneNox = IIf(.applicant_info.landline(nCtr).sPhoneNox <> txtPerso14.Tag, "0", "1")
                    Case 1
                        p_oTrans.Description.applicant_info.landline(nCtr).sPhoneNox = IIf(.applicant_info.landline(nCtr).sPhoneNox <> txtPerso15.Tag, "0", "1")
                    Case 2
                        p_oTrans.Description.applicant_info.landline(nCtr).sPhoneNox = IIf(.applicant_info.landline(nCtr).sPhoneNox <> txtPerso16.Tag, "0", "1")
                End Select
            Next nCtr

            For nCtr As Integer = 0 To .applicant_info.email_address.Count - 1
                p_oTrans.Description.applicant_info.email_address.Add(New GOCASConst.email_param)
                Select Case nCtr
                    Case 0
                        p_oTrans.Description.applicant_info.email_address(nCtr).sEmailAdd = IIf(.applicant_info.email_address(nCtr).sEmailAdd <> txtPerso17.Tag, "0", "1")
                    Case 1
                        p_oTrans.Description.applicant_info.email_address(nCtr).sEmailAdd = IIf(.applicant_info.email_address(nCtr).sEmailAdd <> txtPerso18.Tag, "0", "1")
                    Case 2
                        p_oTrans.Description.applicant_info.email_address(nCtr).sEmailAdd = IIf(.applicant_info.email_address(nCtr).sEmailAdd <> txtPerso19.Tag, "0", "1")
                End Select
            Next nCtr

            p_oTrans.Description.residence_info.cOwnershp = IIf(.residence_info.cOwnershp <> cmb90.Tag, "0", "1")
            p_oTrans.Description.residence_info.cOwnOther = IIf(.residence_info.cOwnOther <> cmb05.Tag, "0", "1")
            p_oTrans.Description.residence_info.cHouseTyp = IIf(.residence_info.cHouseTyp <> cmb07.Tag, "0", "1")
            p_oTrans.Description.residence_info.cGaragexx = IIf(.residence_info.cGaragexx <> cmb06.Tag, "0", "1")

            p_oTrans.Description.residence_info.present_address.sLandMark = IIf(.residence_info.present_address.sLandMark <> txtResid01.Tag, "0", "1")
            p_oTrans.Description.residence_info.present_address.sHouseNox = IIf(.residence_info.present_address.sHouseNox <> txtResid02.Tag, "0", "1")
            p_oTrans.Description.residence_info.present_address.sAddress1 = IIf(.residence_info.present_address.sAddress1 <> txtResid03.Tag, "0", "1")
            p_oTrans.Description.residence_info.present_address.sAddress2 = IIf(.residence_info.present_address.sAddress2 <> txtResid04.Tag, "0", "1")
            p_oTrans.Description.residence_info.rent_others.cRntOther = IIf(.residence_info.rent_others.cRntOther <> cmb91.Tag, "0", "1")
            p_oTrans.Description.residence_info.rent_others.nLenStayx = IIf(.residence_info.rent_others.nLenStayx <> txtResid09.Tag, "0", "1")
            p_oTrans.Description.residence_info.rent_others.nRentExps = IIf(.residence_info.rent_others.nRentExps <> txtResid10.Tag, "0", "1")

            p_oTrans.Description.residence_info.sCtkReltn = IIf(.residence_info.sCtkReltn <> txtResid11.Tag, "0", "1")
            p_oTrans.Description.residence_info.present_address.sTownIDxx = IIf(.residence_info.present_address.sTownIDxx <> txtResid05.Tag, "0", "1")
            p_oTrans.Description.residence_info.present_address.sBrgyIDxx = IIf(.residence_info.present_address.sBrgyIDxx <> txtResid06.Tag, "0", "1")
            p_oTrans.Description.residence_info.permanent_address.sTownIDxx = IIf(.residence_info.permanent_address.sTownIDxx <> txtResid16.Tag, "0", "1")
            p_oTrans.Description.residence_info.permanent_address.sBrgyIDxx = IIf(.residence_info.permanent_address.sBrgyIDxx <> txtResid17.Tag, "0", "1")
            p_oTrans.Description.residence_info.permanent_address.sLandMark = IIf(.residence_info.permanent_address.sLandMark <> txtResid12.Tag, "0", "1")
            p_oTrans.Description.residence_info.permanent_address.sHouseNox = IIf(.residence_info.permanent_address.sHouseNox <> txtResid13.Tag, "0", "1")
            p_oTrans.Description.residence_info.permanent_address.sAddress1 = IIf(.residence_info.permanent_address.sAddress1 <> txtResid14.Tag, "0", "1")
            p_oTrans.Description.residence_info.permanent_address.sAddress2 = IIf(.residence_info.permanent_address.sAddress2 <> txtResid15.Tag, "0", "1")

            p_oTrans.Description.means_info.employed.sIndstWrk = IIf(.means_info.employed.sIndstWrk <> txtEmplo01.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.sEmployer = IIf(.means_info.employed.sEmployer <> txtEmplo02.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.sWrkAddrx = IIf(.means_info.employed.sWrkAddrx <> txtEmplo03.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.sFunction = IIf(.means_info.employed.sFunction <> txtEmplo06.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.nLenServc = IIf(.means_info.employed.nLenServc <> txtEmplo08.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.nSalaryxx = IIf(.means_info.employed.nSalaryxx <> Double.Parse(txtEmplo09.Tag), "0", "1")
            p_oTrans.Description.means_info.employed.sWrkTelno = IIf(.means_info.employed.sWrkTelno <> txtEmplo10.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.sIndstBus = IIf(.means_info.self_employed.sIndstBus <> txtEmplo11.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.sBusiness = IIf(.means_info.self_employed.sBusiness <> txtEmplo12.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.sBusAddrx = IIf(.means_info.self_employed.sBusAddrx <> txtEmplo13.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.nBusLenxx = IIf(.means_info.self_employed.nBusLenxx <> txtEmplo15.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.nBusIncom = IIf(.means_info.self_employed.nBusIncom <> Double.Parse(txtEmplo16.Tag), "0", "1")
            p_oTrans.Description.means_info.self_employed.nMonExpns = IIf(.means_info.self_employed.nMonExpns <> Double.Parse(txtEmplo17.Tag), "0", "1")
            p_oTrans.Description.means_info.financed.sFinancer = IIf(.means_info.financed.sFinancer <> txtEmplo18.Tag, "0", "1")
            p_oTrans.Description.means_info.financed.nEstIncme = IIf(.means_info.financed.nEstIncme <> Double.Parse(txtEmplo19.Tag), "0", "1")
            p_oTrans.Description.means_info.financed.sNatnCode = IIf(.means_info.financed.sNatnCode <> txtEmplo20.Tag, "0", "1")
            p_oTrans.Description.means_info.financed.sMobileNo = IIf(.means_info.financed.sMobileNo <> txtEmplo21.Tag, "0", "1")
            p_oTrans.Description.means_info.financed.sFBAcctxx = IIf(.means_info.financed.sFBAcctxx <> txtEmplo22.Tag, "0", "1")
            p_oTrans.Description.means_info.financed.sEmailAdd = IIf(.means_info.financed.sEmailAdd <> txtEmplo23.Tag, "0", "1")
            p_oTrans.Description.means_info.pensioner.nPensionx = IIf(.means_info.pensioner.nPensionx <> Double.Parse(txtEmplo24.Tag), "0", "1")
            p_oTrans.Description.means_info.pensioner.nRetrYear = IIf(.means_info.pensioner.nRetrYear <> txtEmplo25.Tag, "0", "1")
            p_oTrans.Description.means_info.other_income.sOthrIncm = IIf(.means_info.other_income.sOthrIncm <> txtEmplo26.Tag, "0", "1")
            If Not IsNumeric(txtEmplo27.Tag) Then
                p_oTrans.Description.means_info.other_income.nOthrIncm = IIf(.means_info.other_income.nOthrIncm <> txtEmplo27.Tag, "0", "1")
            Else
                p_oTrans.Description.means_info.other_income.nOthrIncm = IIf(.means_info.other_income.nOthrIncm <> Double.Parse(txtEmplo27.Tag), "0", "1")
            End If
            p_oTrans.Description.means_info.employed.sPosition = IIf(.means_info.employed.sPosition <> txtEmplo05.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.sOFWNatnx = IIf(.means_info.employed.sOFWNatnx <> txtEmplo00.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.sOFWNatnx = IIf(.means_info.employed.sOFWNatnx <> txtEmplo20.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.sWrkTownx = IIf(.means_info.employed.sWrkTownx <> txtEmplo04.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.sBusTownx = IIf(.means_info.self_employed.sBusTownx <> txtEmplo14.Tag, "0", "1")
            p_oTrans.Description.means_info.cIncmeSrc = IIf(.means_info.cIncmeSrc <> cmb08.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cEmpSectr = IIf(.means_info.employed.cEmpSectr <> cmb09.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cUniforme = IIf(.means_info.employed.cUniforme <> cmb10.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cMilitary = IIf(.means_info.employed.cMilitary <> cmb11.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cGovtLevl = IIf(.means_info.employed.cGovtLevl <> cmb12.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cCompLevl = IIf(.means_info.employed.cCompLevl <> cmb13.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cEmpLevlx = IIf(.means_info.employed.cEmpLevlx <> cmb14.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cOcCatgry = IIf(.means_info.employed.cOcCatgry <> cmb15.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cOFWRegnx = IIf(.means_info.employed.cOFWRegnx <> cmb16.Tag, "0", "1")
            p_oTrans.Description.means_info.employed.cEmpStatx = IIf(.means_info.employed.cEmpStatx <> cmb96.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.cBusTypex = IIf(.means_info.self_employed.cBusTypex <> cmb17.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.cOwnTypex = IIf(.means_info.self_employed.cOwnTypex <> cmb18.Tag, "0", "1")
            p_oTrans.Description.means_info.self_employed.cOwnSizex = IIf(.means_info.self_employed.cOwnSizex <> cmb95.Tag, "0", "1")
            p_oTrans.Description.means_info.financed.sReltnCde = IIf(.means_info.financed.sReltnCde <> cmb85.Tag, "0", "1")
            p_oTrans.Description.means_info.pensioner.cPenTypex = IIf(.means_info.pensioner.cPenTypex <> cmb86.Tag, "0", "1")

            p_oTrans.Description.disbursement_info.properties.cWith3Whl = IIf(.disbursement_info.properties.cWith3Whl <> cmb19.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.cWith4Whl = IIf(.disbursement_info.properties.cWith4Whl <> cmb60.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.cWith2Whl = IIf(.disbursement_info.properties.cWith2Whl <> cmb20.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.cWithRefx = IIf(.disbursement_info.properties.cWithRefx <> cmb21.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.cWithTVxx = IIf(.disbursement_info.properties.cWithTVxx <> cmb22.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.cWithACxx = IIf(.disbursement_info.properties.cWithACxx <> cmb23.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.monthly_expenses.nElctrcBl = IIf(.disbursement_info.monthly_expenses.nElctrcBl <> Double.Parse(txtDisbu00.Tag), "0", "1")
            p_oTrans.Description.disbursement_info.monthly_expenses.nWaterBil = IIf(.disbursement_info.monthly_expenses.nWaterBil <> Double.Parse(txtDisbu01.Tag), "0", "1")
            p_oTrans.Description.disbursement_info.monthly_expenses.nFoodAllw = IIf(.disbursement_info.monthly_expenses.nFoodAllw <> Double.Parse(txtDisbu02.Tag), "0", "1")
            p_oTrans.Description.disbursement_info.monthly_expenses.nLoanAmtx = IIf(.disbursement_info.monthly_expenses.nLoanAmtx <> Double.Parse(txtDisbu03.Tag), "0", "1")
            p_oTrans.Description.disbursement_info.properties.sProprty1 = IIf(.disbursement_info.properties.sProprty1 <> txtDisbu04.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.sProprty2 = IIf(.disbursement_info.properties.sProprty2 <> txtDisbu05.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.properties.sProprty3 = IIf(.disbursement_info.properties.sProprty3 <> txtDisbu06.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.bank_account.sBankName = IIf(.disbursement_info.bank_account.sBankName <> txtDisbu07.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.bank_account.sAcctType = IIf(.disbursement_info.bank_account.sAcctType <> cmb24.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.credit_card.sBankName = IIf(.disbursement_info.credit_card.sBankName <> txtDisbu08.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.credit_card.nCrdLimit = IIf(.disbursement_info.credit_card.nCrdLimit <> Double.Parse(txtDisbu09.Tag), "0", "1")
            p_oTrans.Description.disbursement_info.credit_card.nSinceYrx = IIf(.disbursement_info.credit_card.nSinceYrx <> txtDisbu10.Tag, "0", "1")
            p_oTrans.Description.disbursement_info.dependent_info.nHouseHld = IIf(.disbursement_info.dependent_info.nHouseHld <> txtDisbu11.Tag, "0", "1")

            If Trim(dgv00.Rows(0).Cells(1).Value) <> "" Then
                For nCtr As Integer = 0 To .disbursement_info.dependent_info.children.Count - 1
                    p_oTrans.Description.disbursement_info.dependent_info.children.Add(New GOCASConst.children_param)
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sFullName = IIf(.disbursement_info.dependent_info.children(nCtr).sFullName <> txtDisbu12.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).nDepdAgex = IIf(.disbursement_info.dependent_info.children(nCtr).nDepdAgex <> txtDisbu13.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sSchlName = IIf(.disbursement_info.dependent_info.children(nCtr).sSchlName <> txtDisbu14.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sSchlAddr = IIf(.disbursement_info.dependent_info.children(nCtr).sSchlAddr <> txtDisbu15.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sSchlTown = IIf(.disbursement_info.dependent_info.children(nCtr).sSchlTown <> txtDisbu16.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sCompanyx = IIf(.disbursement_info.dependent_info.children(nCtr).sCompanyx <> txtDisbu17.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cIsPupilx = IIf(.disbursement_info.dependent_info.children(nCtr).cIsPupilx <> cmb64.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cIsPrivte = IIf(.disbursement_info.dependent_info.children(nCtr).cIsPrivte <> cmb65.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cIsSchlrx = IIf(.disbursement_info.dependent_info.children(nCtr).cIsSchlrx <> cmb67.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cHasWorkx = IIf(.disbursement_info.dependent_info.children(nCtr).cHasWorkx <> cmb68.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cWorkType = IIf(.disbursement_info.dependent_info.children(nCtr).cWorkType <> cmb69.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cHouseHld = IIf(.disbursement_info.dependent_info.children(nCtr).cHouseHld <> cmb50.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cDependnt = IIf(.disbursement_info.dependent_info.children(nCtr).cDependnt <> cmb51.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cIsChildx = IIf(.disbursement_info.dependent_info.children(nCtr).cIsChildx <> cmb52.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).cIsMarrdx = IIf(.disbursement_info.dependent_info.children(nCtr).cIsMarrdx <> cmb53.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sEducLevl = IIf(.disbursement_info.dependent_info.children(nCtr).sEducLevl <> cmb66.Tag, "0", "1")
                    p_oTrans.Description.disbursement_info.dependent_info.children(nCtr).sRelatnCD = IIf(.disbursement_info.dependent_info.children(nCtr).sRelatnCD <> cmb63.Tag, "0", "1")
                Next nCtr
            End If

            For nCtr As Integer = 0 To .other_info.personal_reference.Count - 1
                p_oTrans.Description.other_info.personal_reference.Add(New GOCASConst.personal_reference_param)
                p_oTrans.Description.other_info.personal_reference(nCtr).sRefrNmex = IIf(.other_info.personal_reference(nCtr).sRefrNmex <> txtOther00.Tag, "0", "1")
                p_oTrans.Description.other_info.personal_reference(nCtr).sRefrMPNx = IIf(.other_info.personal_reference(nCtr).sRefrMPNx <> txtOther01.Tag, "0", "1")
                p_oTrans.Description.other_info.personal_reference(nCtr).sRefrAddx = IIf(.other_info.personal_reference(nCtr).sRefrAddx <> txtOther02.Tag, "0", "1")
                p_oTrans.Description.other_info.personal_reference(nCtr).sRefrTown = IIf(.other_info.personal_reference(nCtr).sRefrTown <> txtOther03.Tag, "0", "1")
            Next

            p_oTrans.Description.other_info.sUnitUser = IIf(.other_info.sUnitUser <> cmb26.Tag, "0", "1")
            p_oTrans.Description.other_info.sUsr2Buyr = IIf(.other_info.sUsr2Buyr <> cmb27.Tag, "0", "1")
            p_oTrans.Description.other_info.sPurposex = IIf(.other_info.sPurposex <> cmb28.Tag, "0", "1")
            p_oTrans.Description.other_info.sUnitPayr = IIf(.other_info.sUnitPayr <> cmb29.Tag, "0", "1")
            p_oTrans.Description.other_info.sPyr2Buyr = IIf(.other_info.sPyr2Buyr <> cmb30.Tag, "0", "1")
            p_oTrans.Description.other_info.sSrceInfo = IIf(.other_info.sSrceInfo <> txtOther04.Tag, "0", "1")

            If Not IsNothing(.spouse_info) Then
                p_oTrans.Description.spouse_info.personal_info.cCvilStat = IIf(.spouse_info.personal_info.cCvilStat <> cmb33.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.cGenderCd = IIf(.spouse_info.personal_info.cGenderCd <> cmb34.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sLastName = IIf(.spouse_info.personal_info.sLastName <> txtSpoIn00.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sFrstName = IIf(.spouse_info.personal_info.sFrstName <> txtSpoIn01.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sSuffixNm = IIf(.spouse_info.personal_info.sSuffixNm <> txtSpoIn02.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sMiddName = IIf(.spouse_info.personal_info.sMiddName <> txtSpoIn03.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sNickName = IIf(.spouse_info.personal_info.sNickName <> txtSpoIn04.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sCitizenx = IIf(.spouse_info.personal_info.sCitizenx <> txtSpoIn07.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sBirthPlc = IIf(.spouse_info.personal_info.sBirthPlc <> txtSpoIn06.Tag, "0", "1")
                If Not IsDate(txtSpoIn05.Tag) Then
                    p_oTrans.Description.spouse_info.personal_info.dBirthDte = IIf(.spouse_info.personal_info.dBirthDte <> txtSpoIn05.Tag, "0", "1")
                Else
                    p_oTrans.Description.spouse_info.personal_info.dBirthDte = IIf(.spouse_info.personal_info.dBirthDte <> txtSpoIn05.Tag, "0", "1")
                End If

                For nCtr As Integer = 0 To .spouse_info.personal_info.mobile_number.Count - 1
                    p_oTrans.Description.spouse_info.personal_info.mobile_number.Add(New GOCASConst.mobileno_param)
                    Select Case nCtr
                        Case 0
                            p_oTrans.Description.spouse_info.personal_info.mobile_number(nCtr).sMobileNo = IIf(.spouse_info.personal_info.mobile_number(nCtr).sMobileNo <> txtSpoIn08.Tag, "0", "1")
                        Case 1
                            p_oTrans.Description.spouse_info.personal_info.mobile_number(nCtr).sMobileNo = IIf(.spouse_info.personal_info.mobile_number(nCtr).sMobileNo <> txtSpoIn09.Tag, "0", "1")
                        Case 2
                            p_oTrans.Description.spouse_info.personal_info.mobile_number(nCtr).sMobileNo = IIf(.spouse_info.personal_info.mobile_number(nCtr).sMobileNo <> txtSpoIn10.Tag, "0", "1")
                    End Select
                Next nCtr

                For nCtr As Integer = 0 To .spouse_info.personal_info.landline.Count - 1
                    p_oTrans.Description.spouse_info.personal_info.landline.Add(New GOCASConst.landline_param)
                    Select Case nCtr
                        Case 0
                            p_oTrans.Description.spouse_info.personal_info.landline(nCtr).sPhoneNox = IIf(.spouse_info.personal_info.landline(nCtr).sPhoneNox <> txtSpoIn11.Tag, "0", "1")
                        Case 1
                            p_oTrans.Description.spouse_info.personal_info.landline(nCtr).sPhoneNox = IIf(.spouse_info.personal_info.landline(nCtr).sPhoneNox <> txtSpoIn12.Tag, "0", "1")
                        Case 2
                            p_oTrans.Description.spouse_info.personal_info.landline(nCtr).sPhoneNox = IIf(.spouse_info.personal_info.landline(nCtr).sPhoneNox <> txtSpoIn13.Tag, "0", "1")
                    End Select
                Next nCtr

                p_oTrans.Description.spouse_info.personal_info.sMaidenNm = IIf(.spouse_info.personal_info.sMaidenNm <> txtSpoIn14.Tag, "0", "1")

                For nctr As Integer = 0 To .spouse_info.personal_info.email_address.Count - 1
                    p_oTrans.Description.spouse_info.personal_info.email_address.Add(New GOCASConst.email_param)
                    Select Case nctr
                        Case 0
                            p_oTrans.Description.spouse_info.personal_info.email_address(nctr).sEmailAdd = IIf(.spouse_info.personal_info.email_address(nctr).sEmailAdd <> txtSpoIn15.Tag, "0", "1")
                        Case 1
                            p_oTrans.Description.spouse_info.personal_info.email_address(nctr).sEmailAdd = IIf(.spouse_info.personal_info.email_address(nctr).sEmailAdd <> txtSpoIn16.Tag, "0", "1")
                        Case 2
                            p_oTrans.Description.spouse_info.personal_info.email_address(nctr).sEmailAdd = IIf(.spouse_info.personal_info.email_address(nctr).sEmailAdd <> txtSpoIn17.Tag, "0", "1")
                    End Select
                Next nctr

                p_oTrans.Description.spouse_info.personal_info.facebook.cAcctStat = IIf(.spouse_info.personal_info.facebook.cAcctStat <> cmb75.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.facebook.sFBAcctxx = IIf(.spouse_info.personal_info.facebook.sFBAcctxx <> txtSpoIn18.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.facebook.nNoFriend = IIf(.spouse_info.personal_info.facebook.nNoFriend <> txtSpoIn21.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.facebook.nYearxxxx = IIf(.spouse_info.personal_info.facebook.nYearxxxx <> txtSpoIn22.Tag, "0", "1")
                p_oTrans.Description.spouse_info.personal_info.sVibeAcct = IIf(.spouse_info.personal_info.sVibeAcct <> txtSpoIn19.Tag, "0", "1")
            End If

            If Not IsNothing(.spouse_info) Then
                If Not IsNothing(.spouse_info.residence_info.present_address) Then
                    p_oTrans.Description.spouse_info.residence_info.present_address.sTownIDxx = IIf(.spouse_info.residence_info.present_address.sTownIDxx <> txtSpoRe04.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.present_address.sBrgyIDxx = IIf(.spouse_info.residence_info.present_address.sBrgyIDxx <> txtSpoRe05.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.present_address.sLandMark = IIf(.spouse_info.residence_info.present_address.sLandMark <> txtSpoRe00.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.present_address.sHouseNox = IIf(.spouse_info.residence_info.present_address.sHouseNox <> txtSpoRe01.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.present_address.sAddress1 = IIf(.spouse_info.residence_info.present_address.sAddress1 <> txtSpoRe02.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.present_address.sAddress2 = IIf(.spouse_info.residence_info.present_address.sAddress2 <> txtSpoRe03.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.rent_others.cRntOther = IIf(.spouse_info.residence_info.rent_others.cRntOther <> cmb84.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.rent_others.nLenStayx = IIf(.spouse_info.residence_info.rent_others.nLenStayx <> txtSpoRe06.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.rent_others.nRentExps = IIf(.spouse_info.residence_info.rent_others.nRentExps <> txtSpoRe07.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.cOwnershp = IIf(.spouse_info.residence_info.cOwnershp <> cmb80.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.cOwnOther = IIf(.spouse_info.residence_info.cOwnOther <> cmb81.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.cGaragexx = IIf(.spouse_info.residence_info.cGaragexx <> cmb82.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.cHouseTyp = IIf(.spouse_info.residence_info.cHouseTyp <> cmb83.Tag, "0", "1")
                End If
                If Not IsNothing(p_oTrans.Category.spouse_info.residence_info.permanent_address) Then
                    p_oTrans.Description.spouse_info.residence_info.permanent_address.sTownIDxx = IIf(.spouse_info.residence_info.permanent_address.sTownIDxx <> txtSpoRe13.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.permanent_address.sBrgyIDxx = IIf(.spouse_info.residence_info.permanent_address.sBrgyIDxx <> txtSpoRe14.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.permanent_address.sLandMark = IIf(.spouse_info.residence_info.permanent_address.sLandMark <> txtSpoRe09.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.permanent_address.sHouseNox = IIf(.spouse_info.residence_info.permanent_address.sHouseNox <> txtSpoRe10.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.permanent_address.sAddress1 = IIf(.spouse_info.residence_info.permanent_address.sAddress1 <> txtSpoRe11.Tag, "0", "1")
                    p_oTrans.Description.spouse_info.residence_info.permanent_address.sAddress2 = IIf(.spouse_info.residence_info.permanent_address.sAddress2 <> txtSpoRe09.Tag, "0", "1")
                End If
                p_oTrans.Description.spouse_info.residence_info.sCtkReltn = IIf(.spouse_info.residence_info.sCtkReltn <> txtSpoRe08.Tag, "0", "1")
            End If

            If Not IsNothing(.spouse_means) Then
                p_oTrans.Description.spouse_means.cIncmeSrc = IIf(.spouse_means.cIncmeSrc <> cmb35.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cEmpSectr = IIf(.spouse_means.employed.cEmpSectr <> cmb36.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cUniforme = IIf(.spouse_means.employed.cUniforme <> cmb37.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cMilitary = IIf(.spouse_means.employed.cMilitary <> cmb38.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cGovtLevl = IIf(.spouse_means.employed.cGovtLevl <> cmb39.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cCompLevl = IIf(.spouse_means.employed.cCompLevl <> cmb40.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cEmpLevlx = IIf(.spouse_means.employed.cEmpLevlx <> cmb41.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cOcCatgry = IIf(.spouse_means.employed.cOcCatgry <> cmb42.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cOFWRegnx = IIf(.spouse_means.employed.cOFWRegnx <> cmb43.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.cEmpStatx = IIf(.spouse_means.employed.cEmpStatx <> cmb97.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.cBusTypex = IIf(.spouse_means.self_employed.cBusTypex <> cmb98.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.cOwnTypex = IIf(.spouse_means.self_employed.cOwnTypex <> cmb99.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.cOwnSizex = IIf(.spouse_means.self_employed.cOwnSizex <> cmb89.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sIndstWrk = IIf(.spouse_means.employed.sIndstWrk <> txtSpoEm00.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sEmployer = IIf(.spouse_means.employed.sEmployer <> txtSpoEm01.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sWrkAddrx = IIf(.spouse_means.employed.sWrkAddrx <> txtSpoEm02.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sFunction = IIf(.spouse_means.employed.sFunction <> txtSpoEm05.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.nLenServc = IIf(.spouse_means.employed.nLenServc <> txtSpoEm07.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.nSalaryxx = IIf(.spouse_means.employed.nSalaryxx <> txtSpoEm08.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sWrkTelno = IIf(.spouse_means.employed.sWrkTelno <> txtSpoEm09.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.sIndstBus = IIf(.spouse_means.self_employed.sIndstBus <> txtSpoEm10.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.sBusiness = IIf(.spouse_means.self_employed.sBusiness <> txtSpoEm11.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.sBusAddrx = IIf(.spouse_means.self_employed.sBusAddrx <> txtSpoEm12.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.nBusLenxx = IIf(.spouse_means.self_employed.nBusLenxx <> txtSpoEm15.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.nBusIncom = IIf(.spouse_means.self_employed.nBusIncom <> txtSpoEm16.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.nMonExpns = IIf(.spouse_means.self_employed.nMonExpns <> txtSpoEm17.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sOFWNatnx = IIf(.spouse_means.employed.sOFWNatnx <> txtSpoEm20.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sWrkTownx = IIf(.spouse_means.employed.sWrkTownx <> txtSpoEm03.Tag, "0", "1")
                p_oTrans.Description.spouse_means.employed.sPosition = IIf(.spouse_means.employed.sPosition <> txtSpoEm04.Tag, "0", "1")
                p_oTrans.Description.spouse_means.self_employed.sBusTownx = IIf(.spouse_means.self_employed.sBusTownx <> txtSpoEm13.Tag, "0", "1")
            End If

            If Not IsNothing(p_oTrans.Description.comaker_info) Then
                p_oTrans.Description.comaker_info.sLastName = IIf(.comaker_info.sLastName <> txtCoMak00.Tag, "0", "1")
                p_oTrans.Description.comaker_info.sFrstName = IIf(.comaker_info.sFrstName <> txtCoMak01.Tag, "0", "1")
                p_oTrans.Description.comaker_info.sMiddName = IIf(.comaker_info.sMiddName <> txtCoMak02.Tag, "0", "1")
                p_oTrans.Description.comaker_info.sSuffixNm = IIf(.comaker_info.sSuffixNm <> txtCoMak03.Tag, "0", "1")
                p_oTrans.Description.comaker_info.sNickName = IIf(.comaker_info.sNickName <> txtCoMak04.Tag, "0", "1")
                p_oTrans.Description.comaker_info.cIncmeSrc = IIf(.comaker_info.cIncmeSrc <> cmb70.Tag, "0", "1")
                p_oTrans.Description.comaker_info.sReltnCde = IIf(.comaker_info.sReltnCde <> cmb71.Tag, "0", "1")
                If Not IsDate(txtCoMak05.Tag) Then
                    p_oTrans.Description.comaker_info.dBirthDte = IIf(.comaker_info.dBirthDte <> txtCoMak05.Tag, "0", "1")
                Else
                    p_oTrans.Description.comaker_info.dBirthDte = IIf(.comaker_info.dBirthDte <> Date.Parse(txtCoMak05.Tag), "0", "1")
                End If
                p_oTrans.Description.comaker_info.sBirthPlc = IIf(p_oTrans.Category.comaker_info.sBirthPlc <> txtCoMak06.Tag, "0", "1")

                For nCtr As Integer = 0 To .comaker_info.mobile_number.Count - 1
                    p_oTrans.Description.comaker_info.mobile_number.Add(New GOCASConst.mobileno_param)
                    Select Case nCtr
                        Case 0
                            p_oTrans.Description.comaker_info.mobile_number(nCtr).sMobileNo = IIf(.comaker_info.mobile_number(nCtr).sMobileNo <> txtCoMak09.Tag, "0", "1")
                        Case 1
                            p_oTrans.Description.comaker_info.mobile_number(nCtr).sMobileNo = IIf(.comaker_info.mobile_number(nCtr).sMobileNo <> txtCoMak11.Tag, "0", "1")
                        Case 2
                            p_oTrans.Description.comaker_info.mobile_number(nCtr).sMobileNo = IIf(.comaker_info.mobile_number(nCtr).sMobileNo <> txtCoMak09.Tag, "0", "1")
                    End Select
                Next nCtr
                p_oTrans.Description.comaker_info.sFBAcctxx = IIf(.comaker_info.sFBAcctxx <> txtCoMak10.Tag, "0", "1")
            End If
            If Not IsNothing(p_oTrans.Description.comaker_info.residence_info.present_address) Then
                p_oTrans.Description.comaker_info.residence_info.present_address.sLandMark = IIf(.comaker_info.residence_info.present_address.sLandMark <> txtCoAdd00.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.present_address.sHouseNox = IIf(.comaker_info.residence_info.present_address.sHouseNox <> txtCoAdd01.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.present_address.sAddress1 = IIf(.comaker_info.residence_info.present_address.sAddress1 <> txtCoAdd02.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.present_address.sAddress2 = IIf(.comaker_info.residence_info.present_address.sAddress2 <> txtCoAdd03.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.present_address.sTownIDxx = IIf(.comaker_info.residence_info.present_address.sTownIDxx <> txtCoAdd04.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.present_address.sBrgyIDxx = IIf(.comaker_info.residence_info.present_address.sBrgyIDxx <> txtCoAdd05.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.rent_others.nLenStayx = IIf(.comaker_info.residence_info.rent_others.nLenStayx <> txtCoAdd06.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.rent_others.nRentExps = IIf(.comaker_info.residence_info.rent_others.nRentExps <> txtCoAdd07.Tag, "0", "1")
                p_oTrans.Description.comaker_info.residence_info.sCtkReltn = IIf(.comaker_info.residence_info.sCtkReltn <> txtCoAdd08.Tag, "0", "1")
            End If
        End With
    End Sub

    'Handles Validating Events for txtField & txtField
    Private Sub txtIntro_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        With p_oTrans.Category
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
                End Select
            End If
        End With
    End Sub

    Private Sub txtOther_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        pdRow = 0
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.other_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtOther" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 0
                        .personal_reference(pdRow).sRefrNmex = loTxt.Text
                    Case 1
                        .personal_reference(pdRow).sRefrMPNx = loTxt.Text
                    Case 2
                        .personal_reference(pdRow).sRefrAddx = loTxt.Text
                    Case 4
                        .sSrceInfo = loTxt.Text
                End Select
            End If
        End With
    End Sub

    Private Sub txtSpoIn_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If IsNothing(p_oTrans.Category.spouse_info) Then Exit Sub
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.spouse_info.personal_info
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
                        If .mobile_number.Count = 0 Then .mobile_number.Add(New GOCASConst.mobileno_param)
                        .mobile_number(0).sMobileNo = loTxt.Text
                    Case 9
                        If .mobile_number.Count = 0 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 1 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        End If
                        .mobile_number(1).sMobileNo = loTxt.Text
                    Case 10
                        If .mobile_number.Count = 0 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 1 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 2 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        End If
                        .mobile_number(2).sMobileNo = loTxt.Text
                    Case 11
                        If .landline.Count = 0 Then .landline.Add(New GOCASConst.landline_param)
                        .landline(0).sPhoneNox = loTxt.Text
                    Case 12
                        If .landline.Count = 0 Then
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                        ElseIf .landline.Count = 1 Then
                            .landline.Add(New GOCASConst.landline_param)
                        End If
                        .landline(1).sPhoneNox = loTxt.Text
                    Case 13
                        If .landline.Count = 0 Then
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                        ElseIf .landline.Count = 1 Then
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                        ElseIf .landline.Count = 2 Then
                            .landline.Add(New GOCASConst.landline_param)
                        End If
                        .landline(2).sPhoneNox = loTxt.Text
                    Case 14
                        .sMaidenNm = loTxt.Text
                    Case 15
                        If .email_address.Count = 0 Then .email_address.Add(New GOCASConst.email_param)
                        .email_address(0).sEmailAdd = loTxt.Text
                    Case 16
                        If .email_address.Count = 0 Then
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                        ElseIf .email_address.Count = 1 Then
                            .email_address.Add(New GOCASConst.email_param)
                        End If
                        .email_address(1).sEmailAdd = loTxt.Text
                    Case 17
                        If .email_address.Count = 0 Then
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                        ElseIf .email_address.Count = 1 Then
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                        ElseIf .email_address.Count = 2 Then
                            .email_address.Add(New GOCASConst.email_param)
                        End If
                        .email_address(2).sEmailAdd = loTxt.Text
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
        If IsNothing(p_oTrans.Category.spouse_means) Then Exit Sub
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.spouse_means
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
        With p_oTrans.Category.comaker_info
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
                        If .mobile_number.Count = 0 Then .mobile_number.Add(New GOCASConst.mobileno_param)
                        .mobile_number(0).sMobileNo = loTxt.Text
                    Case 11
                        If .mobile_number.Count = 0 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 1 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        End If
                        .mobile_number(1).sMobileNo = loTxt.Text
                    Case 12
                        If .mobile_number.Count = 0 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 1 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 2 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        End If
                        .mobile_number(2).sMobileNo = txtCoMak12.Text
                    Case 10
                        .sFBAcctxx = loTxt.Text
                End Select
            End If
        End With
    End Sub

    Private Sub txtPerso_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.applicant_info
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
                    Case 8, 11
                        If .mobile_number.Count = 0 Then .mobile_number.Add(New GOCASConst.mobileno_param)
                        .mobile_number(0).sMobileNo = txtPerso08.Text
                        .mobile_number(0).nPostYear = txtPerso11.Text
                        .mobile_number(0).cPostPaid = IIf(chk00.CheckState = CheckState.Checked, "1", "0")
                    Case 9, 12
                        If .mobile_number.Count = 0 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 1 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        End If
                        .mobile_number(1).sMobileNo = txtPerso09.Text
                        .mobile_number(1).nPostYear = txtPerso12.Text
                        .mobile_number(1).cPostPaid = IIf(chk01.CheckState = CheckState.Checked, "1", "0")
                    Case 10, 13
                        If .mobile_number.Count = 0 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 1 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        ElseIf .mobile_number.Count = 2 Then
                            .mobile_number.Add(New GOCASConst.mobileno_param)
                        End If
                        .mobile_number(2).sMobileNo = txtPerso10.Text
                        .mobile_number(2).nPostYear = txtPerso13.Text
                        .mobile_number(2).cPostPaid = IIf(chk02.CheckState = CheckState.Checked, "1", "0")
                    Case 14
                        If .landline.Count = 0 Then .landline.Add(New GOCASConst.landline_param)
                        .landline(0).sPhoneNox = loTxt.Text
                    Case 15
                        If .landline.Count = 0 Then
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                        ElseIf .landline.Count = 1 Then
                            .landline.Add(New GOCASConst.landline_param)
                        End If
                        .landline(1).sPhoneNox = loTxt.Text
                    Case 16
                        If .landline.Count = 0 Then
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                        ElseIf .landline.Count = 1 Then
                            .landline.Add(New GOCASConst.landline_param)
                            .landline.Add(New GOCASConst.landline_param)
                        ElseIf .landline.Count = 2 Then
                            .landline.Add(New GOCASConst.landline_param)
                        End If
                        .landline(2).sPhoneNox = loTxt.Text
                    Case 17
                        If .email_address.Count = 0 Then .email_address.Add(New GOCASConst.email_param)
                        .email_address(0).sEmailAdd = loTxt.Text
                    Case 18
                        If .email_address.Count = 0 Then
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                        ElseIf .email_address.Count = 1 Then
                            .email_address.Add(New GOCASConst.email_param)
                        End If
                        .email_address(1).sEmailAdd = loTxt.Text
                    Case 19
                        If .email_address.Count = 0 Then
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                        ElseIf .email_address.Count = 1 Then
                            .email_address.Add(New GOCASConst.email_param)
                            .email_address.Add(New GOCASConst.email_param)
                        ElseIf .email_address.Count = 2 Then
                            .email_address.Add(New GOCASConst.email_param)
                        End If
                        .email_address(2).sEmailAdd = loTxt.Text
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


    Private Sub txtResid_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.residence_info
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

    Private Sub txtSpoRe_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        If IsNothing(p_oTrans.Category.spouse_info.residence_info) Then Exit Sub
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.spouse_info.residence_info
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
                        If (Not IsNothing(.rent_others)) Then
                            If Not IsNumeric(loTxt.Text) Then
                                loTxt.Text = 0
                            Else
                                loTxt.Text = CInt(loTxt.Text)
                            End If
                            .rent_others.nLenStayx = CInt(loTxt.Text)
                        End If
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

    Private Sub txtDisbu_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.disbursement_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtDisbu" And loTxt.ReadOnly = False Then

                Select Case loIndex
                    Case 0
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .monthly_expenses.nElctrcBl = CDbl(loTxt.Text)
                    Case 1
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .monthly_expenses.nWaterBil = CDbl(loTxt.Text)
                    Case 2
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .monthly_expenses.nFoodAllw = CDbl(loTxt.Text)
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .monthly_expenses.nLoanAmtx = CDbl(loTxt.Text)
                    Case 4
                        .properties.sProprty1 = loTxt.Text
                    Case 5
                        .properties.sProprty2 = loTxt.Text
                    Case 6
                        .properties.sProprty3 = loTxt.Text
                    Case 7
                        .bank_account.sBankName = loTxt.Text
                    Case 8
                        .credit_card.sBankName = loTxt.Text
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
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
                        .dependent_info.children(pnRow).sFullName = txtDisbu12.Text
                        .dependent_info.children(pnRow).nDepdAgex = txtDisbu13.Text
                        .dependent_info.children(pnRow).sSchlName = txtDisbu14.Text
                        .dependent_info.children(pnRow).sSchlAddr = txtDisbu15.Text
                        .dependent_info.children(pnRow).sCompanyx = txtDisbu17.Text
                        Call loadDependent_Info()
                End Select
            End If
        End With
    End Sub


    Private Sub combobox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As ComboBox
        loChk = CType(sender, System.Windows.Forms.ComboBox)

        On Error Resume Next
        Dim lnIndex As Integer
        With p_oTrans.Category
            lnIndex = Val(Mid(loChk.Name, 4))
            Select Case lnIndex
                Case 0
                    .cUnitAppl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 1
                    .cApplType = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 19
                    .disbursement_info.properties.cWith3Whl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 60
                    .disbursement_info.properties.cWith4Whl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 20
                    .disbursement_info.properties.cWith2Whl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 21
                    .disbursement_info.properties.cWithRefx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 22
                    .disbursement_info.properties.cWithTVxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 23
                    .disbursement_info.properties.cWithACxx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
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
                    .disbursement_info.dependent_info.children(pnRow).sRelatnCD = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 64
                    .disbursement_info.dependent_info.children(pnRow).cIsPupilx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 65
                    .disbursement_info.dependent_info.children(pnRow).cIsPrivte = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 66
                    .disbursement_info.dependent_info.children(pnRow).sEducLevl = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 67
                    .disbursement_info.dependent_info.children(pnRow).cIsSchlrx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 68
                    .disbursement_info.dependent_info.children(pnRow).cHasWorkx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 69
                    .disbursement_info.dependent_info.children(pnRow).cWorkType = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 50
                    .disbursement_info.dependent_info.children(pnRow).cHouseHld = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 51
                    .disbursement_info.dependent_info.children(pnRow).cDependnt = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 52
                    .disbursement_info.dependent_info.children(pnRow).cIsChildx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 53
                    .disbursement_info.dependent_info.children(pnRow).cIsMarrdx = IIf(loChk.SelectedItem.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 54
                    .comaker_info.residence_info.cOwnershp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 55
                    .comaker_info.residence_info.cOwnOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 56
                    .comaker_info.residence_info.rent_others.cRntOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 57
                    .comaker_info.residence_info.cHouseTyp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 58
                    .comaker_info.residence_info.cGaragexx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
            End Select
        End With
    End Sub

    Private Sub txtEmplo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.means_info
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
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
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
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .self_employed.nBusIncom = CDbl(loTxt.Text)
                    Case 17
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .self_employed.nMonExpns = CDbl(loTxt.Text)
                    Case 18
                        .financed.sFinancer = loTxt.Text
                    Case 19
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
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
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
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
                            loTxt.Text = Format(CDbl(0), xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
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
        cmdButton06.Visible = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
        cmdButton09.Visible = Not lbShow
        cmdButton01.Visible = Not lbShow
        cmdButton00.Visible = Not lbShow
        cmdButton12.Visible = Not lbShow
        cmdButton14.Visible = Not lbShow
        cmdButton15.Visible = Not lbShow
        cmdButton25.Visible = Not lbShow

        cmdButton02.Visible = lbShow
        cmdButton11.Visible = lbShow
        cmdButton12.Visible = lbShow
        listItem.Visible = lbShow
        Panel1.Enabled = lbShow


        If psButton = "1" Then
            cmdButton12.Visible = lbShow
        End If
    End Sub

    Private Sub initDisplay()
        'this will add items to list view.
        With listItem
            .Columns.Clear()
            .Items.Clear()

            listItem.View = View.Details
            .Columns.Add("Number", 90, HorizontalAlignment.Left)
            .Columns.Add("Name", 150, HorizontalAlignment.Left)
        End With
    End Sub

    Private Sub displayMobile(ByVal fsValue As String())
        Dim listOfMobile As String()
        listOfMobile = fsValue
        With listItem
            .Items.Clear()
            For lnCtr As Integer = 0 To listOfMobile.Length - 1
                .Items.Add(listOfMobile(lnCtr)).SubItems.Add(IFNull(p_oTrans.Master("sClientNm"), ""))
            Next lnCtr
        End With
    End Sub

    Private Sub displayRederence(ByVal fsValue As String, ByVal fsName As String)
        With listItem
            .Items.Clear()
            .Items.Add(fsValue).SubItems.Add(fsName)
        End With
        psMobile = fsValue
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        Dim lsName As String
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))
        With p_oTrans
            Select Case lnIndex
                Case 6 'Pick Up
                    ClearFields(Me.Panel1)
                    ClearFields(Me.Panel2)

                    psTransNox = p_oTrans.getNextCustomer
                    If psTransNox = "" Then
                        MsgBox("All applications was successfully verified. Please try again later!!!", MsgBoxStyle.Exclamation, "Warning")
                        Exit Sub
                    End If

                    If p_oTrans.OpenTransaction(psTransNox) = True Then
                        loadTransaction()
                    End If
                Case 7 'Browse
                    If p_oTrans.SearchTransaction("%", False, True) = True Then
                        ClearFields(Me.Panel1)
                        ClearFields(Me.Panel2)
                        loadTransaction()
                    End If
                Case 8 'Customer
                    If txtField00.Text <> "" Then
                        If IsDBNull(p_oTrans.Master("sCatInfox")) Then GoTo moveTo
                        If Not p_oTrans.Master("sCatInfox").Equals("") Then
                            MsgBox("The customer was already called.", MsgBoxStyle.Exclamation, "Notice")
                            Exit Sub
                        End If
moveTo:
                        displayMobile(p_oTrans.callApplicant)
                        psButton = "0"
                        initButton(1)
                    End If
                Case 9 'RefNoloa
                    If txtField00.Text <> "" Then
                        If IFNull(p_oTrans.Master("sCatInfox"), "") = "" Then
                            MsgBox("Please call the customer first.", MsgBoxStyle.Exclamation, "Notice")
                            Exit Sub
                        ElseIf Not IsDBNull(p_oTrans.Master("nCrdtScrx")) Then
                            MsgBox("Credit score was already computed for this application.", MsgBoxStyle.Exclamation, "Notice")
                            Exit Sub
                        End If

                        displayRederence(p_oTrans.callReference(lsName), lsName)

                        If psMobile = "" Then
                            MsgBox("All reference number was already called.", MsgBoxStyle.Exclamation, "Notice")
                            Exit Sub
                        End If

                        psButton = "1"
                        initButton(1)
                    End If
                Case 1 ' credit Score
                    computeCreditScore()
                Case 25
                    If txtField00.Text = "" Then Exit Sub
                    p_oValidate.TransNo = txtField00.Text

                    If IFNull(p_oTrans.Master("sCatInfox"), "") = "" Then
                        MsgBox("Please make sure to load application that was evaluated first.", MsgBoxStyle.Exclamation, "Notice")
                        Exit Sub
                        'ElseIf Not p_oTrans.isReferenceOK Then
                        '    MsgBox("Not all reference numbers are called!", MsgBoxStyle.Exclamation, "Notice")
                        '    Exit Sub
                    Else
                        displayRederence(p_oTrans.callReference(lsName), lsName)

                        If psMobile <> "" Then
                            MsgBox("Not all reference numbers are called!", MsgBoxStyle.Exclamation, "Notice")
                            Exit Sub
                        End If
                    End If

                    Select Case p_oValidate.isRecordExist()
                        Case 0
                            If MsgBox("Do you want to set For-CI validation?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                                showCITagging()
                            End If
                        Case 1
                            showCITaggingView()
                    End Select
                Case 0 ' Exit
                    Me.Dispose()
                Case 2 'save
                    If p_oTrans.Category.applicant_info.cCvilStat <> "1" And p_oTrans.Category.applicant_info.cCvilStat <> "5" Then
                        If isWithSpouse(grpBox17) = False Then Exit Sub
                        If isWithSpouse(grpBox19) = False Then Exit Sub
                        If isWithSpouse(grpBox21) = False Then Exit Sub
                    End If

                    If DataComplete() = False Then Exit Sub

                    If MsgBox("Do you want to save this application??", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                        If psButton = 0 Then
                            isEntryOk()
                            If p_oTrans.confirmTransaction Then
                                MsgBox("Application Successfully Save!!", vbInformation, "Information")
                                initButton(0)
                                ClearFields(Me.Panel1)
                                ClearFields(Me.Panel2)
                                p_oTrans.OpenTransaction(p_oTrans.Master("sTransNox"))
                                loadTransaction()
                            End If
                        ElseIf psButton = 1 Then
                            If p_oTrans.saveReference(psMobile) Then
                                MsgBox("Application Successfully Save!!", vbInformation, "Information")

                                initButton(0)
                                ClearFields(Me.Panel1)
                                ClearFields(Me.Panel2)
                                p_oTrans.OpenTransaction(p_oTrans.Master("sTransNox"))
                                loadTransaction()
                            End If
                        End If
                    End If
                Case 12 'Next Reference
                    If IFNull(p_oTrans.Master("sCatInfox"), "") = "" Then
                        MsgBox("This is evaluation for customer cannot evaluate at one time for reference!", MsgBoxStyle.Exclamation, "Warning")
                        Exit Sub
                    End If
                    Call displayRederence(p_oTrans.getNextReference(psMobile, lsName), lsName)
                Case 11 ' cancel
                    If MsgBox("Do you want to disregard all changes for this application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                        Call ClearFields(Me.Panel1)
                        Call ClearFields(Me.Panel2)
                        clearDependent()
                        initButton(0)
                    End If
                Case 4 'add
                    If Trim(dgv00.Rows(dgv00.Rows.Count - 1).Cells(1).Value) <> "" Then
                        .Category.disbursement_info.dependent_info.children.Add(New GOCASConst.children_param)
                        dgv00.Rows.Add()
                        clearDependent()
                        Call loadDependent_Info()
                        dgv00.CurrentCell = dgv00(0, Me.dgv00.RowCount - 1)
                        dgv00_Click(sender, New System.EventArgs())
                    End If

                Case 5 'delete
                    If dgv00.RowCount - 1 > 0 Then
                        If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            .Category.disbursement_info.dependent_info.children.RemoveAt(pnRow)
                            dgv00.CurrentCell = dgv00(0, 0)
                            dgv00_Click(sender, New System.EventArgs())
                            Call loadDependent_Info()
                        End If
                    Else
                        MessageBox.Show("Cannot delete last data" + Environment.NewLine + "Please use void button to cancel " + Environment.NewLine + "this transaction if save!", "Invalid transaction",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Case 14 'result
                    Call showResult()
                Case 15 'void application
                    If txtField00.Text = "" Then Exit Sub
                    'Dim lsSQL As String
                    'lsSQL = "SELECT" & _
                    '            " sPositnID" & _
                    '        " FROM Employee_Master001 a" & _
                    '            ", xxxSysUser b" & _
                    '        " WHERE a.sEmployID = b.sEmployNo" & _
                    '        " AND b.sUserIDxx=" & strParm(p_oAppDriver.UserID)
                    'Dim loDT As New DataTable
                    'Try
                    '    loDT = p_oAppDriver.ExecuteQuery(lsSQL)
                    '    If loDT.Rows.Count = 1 Then
                    '        If loDT.Rows(0).Item("sPositnID") <> "126" And loDT.Rows(0).Item("sPositnID") <> "098" Then
                    '            MsgBox("You are not allowed to void this application!!" + Environment.NewLine + "Please request assistance for supervisor.", vbCritical, "Warning-" & p_oAppDriver.UserID)
                    '            Exit Sub
                    '        End If
                    '    End If
                    'Catch ex As Exception
                    '    MsgBox(ex.Message)
                    '    Exit Sub
                    'End Try

                    If MsgBox("Do you want to void this credit application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        If p_oTrans.CancelTransaction Then
                            MsgBox("Credit Application successfully voided!!", vbInformation, "Information")
                            Call p_oTrans.NewTransaction()
                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            initButton(0)
                        End If
                    End If
            End Select
        End With
    End Sub

    Private Sub computeCreditScore()
        If txtField00.Text = "" Then Exit Sub

        If IFNull(p_oTrans.Master("sCatInfox"), "") = "" Then
            MsgBox("Please make sure to load application that was evaluated first.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        ElseIf Not p_oTrans.isReferenceOK Then
            MsgBox("Not all reference numbers are called!", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        ElseIf Not IsDBNull(p_oTrans.Master("nCrdtScrx")) Then
            MsgBox("Credit score was already computed for this application.", MsgBoxStyle.Exclamation, "Notice")
            Exit Sub
        End If

        If MsgBox("Do you want to compute credit score?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.Yes Then
            p_oValidate.TransNo = txtField00.Text
            If p_oValidate.isRecordExist = 0 Then
                If MsgBox("For-CI validation not set, do you want create?", vbYesNo + MsgBoxStyle.Information, "Confirmation") = MsgBoxResult.Yes Then
                    showCITagging()
                    Exit Sub
                Else
                    GoTo createScore
                End If
            End If

            Select Case p_oValidate.Others("cTranStat")
                Case "0"
                    MsgBox("For-CI validation is still open. Please wait for CI Result before scoring.", vbInformation, "Information")
                    'Case "1"
                    '    MsgBox("For-CI validation is still on-process. Please wait for CI Result before scoring.", vbInformation, "Information")
                Case Else
createScore:
                    If p_oTrans.Approved(True) Then
                        MsgBox("Application successfully created credit score!!!", vbInformation, "Information")
                        ClearFields(Me.Panel1)
                        ClearFields(Me.Panel2)
                        p_oTrans.OpenTransaction(p_oTrans.Master("sTransNox"))
                        loadTransaction()
                    End If
            End Select
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

    Private Sub loadTransaction()
        loadMainInfo(Me.grpBox24)
        loadIntroQuestion(Me.tabPage00)
        loadAppliInfo(Me.tabPage01)
        loadAppliRes(Me.tabPage02)
        loadAppEmplymnt(Me.tabPage03)
        loadAppEmplymnt(Me.tabPage10)
        loadDisburesement(Me.tabPage04)
        loadDisburesement(Me.tabPage11)
        loadOthers(Me.tabPage05)
        loadSpouseInfo(Me.tabPage06)
        loadSpouseRes(Me.tabPage09)
        loadSpouseEmpl(Me.tabPage07)
        loadComaker(Me.tabPage08)
        loadComakRes(Me.tabPage12)
        setTranStat(IFNull(p_oTrans.Master("cTranStat"), "-1"), lblStatus)
        Call loadDependent_Info()
        Call loadReference_Info()
        Call showResult()
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
                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 80, True, False), True) = True Then
                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            loadTransaction()
                        End If
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtOther" Then
                Select Case loIndex
                    Case 3
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.other_info.personal_reference(pdRow).sRefrTown)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtIntro" Then
                Select Case loIndex
                    Case 0
                        loTxt.Text = p_oTrans.getModel(loTxt.Text, True, False, p_oTrans.Category.sModelIDx)
                    Case 5
                        loTxt.Text = p_oTrans.getBranch(loTxt.Text, True, False, p_oTrans.Category.sBranchCd)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtPerso" Then
                Select Case loIndex
                    Case 6
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.applicant_info.sBirthPlc)
                    Case 7
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.applicant_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtResid" Then
                Select Case loIndex
                    Case 5
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.residence_info.present_address.sTownIDxx)
                    Case 6
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.residence_info.present_address.sBrgyIDxx, p_oTrans.Category.residence_info.present_address.sTownIDxx)
                    Case 16
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.residence_info.permanent_address.sTownIDxx)
                    Case 17
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.residence_info.permanent_address.sBrgyIDxx, p_oTrans.Category.residence_info.permanent_address.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtCoMak" Then
                Select Case loIndex
                    Case 6
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.comaker_info.sBirthPlc)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoIn" Then
                Select Case loIndex
                    Case 6
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_info.personal_info.sBirthPlc)
                    Case 7
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.spouse_info.personal_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoRe" Then
                Select Case loIndex
                    Case 4
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.present_address.sTownIDxx)
                    Case 5
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.present_address.sBrgyIDxx, p_oTrans.Category.spouse_info.residence_info.present_address.sTownIDxx)
                    Case 13
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.permanent_address.sTownIDxx)
                    Case 14
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.permanent_address.sBrgyIDxx, p_oTrans.Category.spouse_info.residence_info.permanent_address.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoEm" Then
                Select Case loIndex
                    Case 3
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_means.employed.sWrkTownx)
                    Case 4
                        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Category.spouse_means.employed.sPosition)
                    Case 13
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_means.self_employed.sBusTownx)
                    Case 20
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.spouse_means.employed.sOFWNatnx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtEmplo" Then
                Select Case loIndex
                    Case 0
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sOFWNatnx)
                    Case 5
                        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sPosition)
                    Case 4
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sWrkTownx)
                    Case 14
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.means_info.self_employed.sBusTownx)
                    Case 20
                        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.means_info.financed.sNatnCode)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtDisbu" Then
                Select Case loIndex
                    Case 16
                        If p_oTrans.Category.disbursement_info.dependent_info.children.Count = 0 Then
                            p_oTrans.Category.disbursement_info.dependent_info.children.Add(New GOCASConst.children_param)
                        End If
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.disbursement_info.dependent_info.children(pnRow).sSchlTown)
                        Call loadDependent_Info()
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtCoAdd" Then
                Select Case loIndex
                    Case 4
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.comaker_info.residence_info.present_address.sTownIDxx)
                    Case 5
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.comaker_info.residence_info.present_address.sBrgyIDxx, p_oTrans.Category.comaker_info.residence_info.present_address.sTownIDxx)
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
                If (TypeOf loTxt Is Label) Then
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "lb1Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb2Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb3Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb4Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb5Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb6Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb7Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb8Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lb9Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "l10Field" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblAddrs" Then
                        Select Case loIndex
                            Case Else
                                loTxt.Text = "N/A"
                        End Select
                    End If
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
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtCoAdd" Then
                            Select Case loIndex
                                Case Else
                                    loTxt.Text = ""
                                    loTxt.Tag = ""
                            End Select
                        End If
                    Else
                        If (TypeOf loTxt Is ComboBox) Then
                            DirectCast(loTxt, ComboBox).SelectedIndex = -1
                            DirectCast(loTxt, ComboBox).Tag = ""
                        End If
                    End If
                End If
            End If
        Next
        pnRow = 0
        pdRow = 0
        psButton = -1
        psMobile = ""
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
                        With p_oTrans.Category
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")
                                    loTxt.Tag = .sModelIDx
                                Case 1
                                    loTxt.Text = Format(CDbl(.nDownPaym), xsDECIMAL)
                                    loTxt.Tag = .nDownPaym
                                Case 2
                                    loTxt.Text = CInt(.nAcctTerm)
                                    loTxt.Tag = .nAcctTerm
                                Case 3
                                    loTxt.Text = Format(CDbl(.nMonAmort), xsDECIMAL)
                                    loTxt.Tag = .nMonAmort
                                Case 4
                                    loTxt.Text = .sUnitAppl
                                    loTxt.Tag = .sUnitAppl
                                Case 5
                                    loTxt.Text = p_oTrans.getBranch(.sBranchCd, False, True, "")
                                    loTxt.Tag = .sBranchCd
                                Case 6
                                    If .dTargetDt = "" Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = Format(CDate(.dTargetDt), xsDATE_MEDIUM)
                                    End If
                                    loTxt.Tag = .dTargetDt
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb1Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail, p_oTrans.Category)
                                Select Case loIndex
                                    Case 2
                                        loTxt.Text = IIf(.sUnitAppl = "", "N/A", .sUnitAppl)
                                    Case 3
                                        loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")
                                    Case 4
                                        loTxt.Text = IIf(.nDownPaym = "", "N/A", Format(CDbl(.nDownPaym), xsDECIMAL))
                                    Case 5
                                        loTxt.Text = IIf(.nAcctTerm = "", "N/A", .nAcctTerm + " Months")
                                    Case 6
                                        loTxt.Text = IIf(.nMonAmort = "", "N/A", Format(CDbl(.nMonAmort), xsDECIMAL))
                                    Case 7
                                        loTxt.Text = IIf(.sBranchCd = "", "N/A", p_oTrans.getBranch(.sBranchCd, False, True, ""))
                                    Case 8
                                        If .dTargetDt = "" Then
                                            loTxt.Text = "N/A"
                                        Else
                                            loTxt.Text = Format(CDate(.dTargetDt), xsDATE_MEDIUM)
                                        End If
                                End Select
                                If (.cUnitAppl <> "") Then setApplicationType(.cUnitAppl, cmb00, lb1Field00)
                                cmb00.Tag = .cUnitAppl
                                If (.cApplType <> "") Then setTypeOfCustomer(.cApplType, cmb01, lb1Field01)
                                cmb01.Tag = .cApplType
                            End With
                        End If
                    End If
                End If
            End If
        Next
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
                                Case 1
                                    loTxt.Text = .getBranch(IFNull(.Master("sBranchCd"), ""), False, True, "")
                                Case 2
                                    loTxt.Text = Format(IFNull(.Master("dTransact"), p_oAppDriver.getSysDate), xsDATE_MEDIUM)
                                Case 4
                                    loTxt.Text = IFNull(.Master("sClientNm"), "")
                                Case 90
                                    If .Category.applicant_info.dBirthDte = "" Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = Format(CDate(IIf(Not IsDate(.Category.applicant_info.dBirthDte), p_oAppDriver.getSysDate, .Category.applicant_info.dBirthDte)), xsDATE_MEDIUM)
                                    End If
                                Case 91
                                    If Not IsDate(.Category.applicant_info.dBirthDte) Then
                                        loTxt.Text = Format(DateDiff("M", p_oAppDriver.getSysDate, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    Else
                                        loTxt.Text = Format(DateDiff("M", .Category.applicant_info.dBirthDte, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    End If
                                Case 92
                                    loTxt.Text = .getTownCity(.Detail.applicant_info.sBirthPlc, False, True, "")
                                Case 95
                                    loTxt.Text = .Category.residence_info.present_address.sHouseNox + " " + _
                                        p_oTrans.getTownCity(.Category.residence_info.present_address.sTownIDxx, False, True, "") + " " + .Category.residence_info.present_address.sAddress1
                            End Select
                        End With
                    End If
                End If
            End If
        Next

        p_oValidate = New GOCASCI(p_oAppDriver)
        p_oValidate.TransNo = txtField00.Text
    End Sub

    Private Sub showResult()
        Dim loFrm = New frmMCCreditAppResult
        If Not IsDBNull(p_oTrans.Master("nCrdtScrx")) And Not IsDBNull(p_oTrans.Master("sGOCASNox")) Then
            loFrm.clearFields()
            loFrm.GoCasNo = IFNull(p_oTrans.Master("sGOCASNox"), "")
            loFrm.sTransNox = IFNull(p_oTrans.Master("sTransNox"), "")
            loFrm.CreditScore = IFNull(p_oTrans.Master("nCrdtScrx"), "")
            loFrm.WithCI = IFNull(p_oTrans.Master("cWithCIxx"), "")
            loFrm.DownPayment = IIf(p_oTrans.Master("nDownPaym") = 200, "DEFAULT", p_oTrans.Master("nDownPaym") & "%")
            loFrm.ShowDialog()
        End If
    End Sub

    Private Sub showCITagging()
        Dim loFrms = New FrmCITagging
        If Not IsDBNull(p_oTrans.Master("sTransNox")) Then
            loFrms.sTransNox = IFNull(p_oTrans.Master("sTransNox"), "")
            loFrms.ShowDialog()
        End If
    End Sub
    Private Sub showCITaggingView()
        Dim loFrm1 = New frmCITaggingViewing
        If Not IsDBNull(p_oTrans.Master("sTransNox")) Then
            loFrm1.sTransNox = IFNull(p_oTrans.Master("sTransNox"), "")
            loFrm1.ShowDialog()
        End If

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
                        With p_oTrans.Category.applicant_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName
                                    loTxt.Tag = .sLastName
                                Case 1
                                    loTxt.Text = .sFrstName
                                    loTxt.Tag = .sFrstName
                                Case 2
                                    loTxt.Text = .sMiddName
                                    loTxt.Tag = .sMiddName
                                Case 3
                                    loTxt.Text = .sSuffixNm
                                    loTxt.Tag = .sSuffixNm
                                Case 4
                                    loTxt.Text = .sNickName
                                    loTxt.Tag = .sNickName
                                Case 5
                                    If Not IsDate(.dBirthDte) Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                    End If
                                    loTxt.Tag = .dBirthDte
                                Case 6
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                    loTxt.Tag = .sBirthPlc
                                Case 7
                                    loTxt.Text = p_oTrans.getCountry(.sCitizenx, False, True, "")
                                    loTxt.Tag = .sCitizenx
                                Case 8
                                    For lnCtr As Integer = 0 To .mobile_number.Count - 1
                                        Select Case lnCtr
                                            Case 0
                                                loTxt.Text = .mobile_number(lnCtr).sMobileNo
                                                loTxt.Tag = .mobile_number(lnCtr).sMobileNo
                                                txtPerso11.Text = .mobile_number(lnCtr).nPostYear
                                                txtPerso11.Tag = .mobile_number(lnCtr).nPostYear
                                                chk00.CheckState = IIf(.mobile_number(lnCtr).cPostPaid = "1", CheckState.Checked, CheckState.Unchecked)
                                                chk00.Tag = .mobile_number(lnCtr).cPostPaid
                                            Case 1
                                                txtPerso09.Text = .mobile_number(lnCtr).sMobileNo
                                                txtPerso09.Tag = .mobile_number(lnCtr).sMobileNo
                                                txtPerso12.Text = .mobile_number(lnCtr).nPostYear
                                                txtPerso12.Tag = .mobile_number(lnCtr).nPostYear
                                                chk01.CheckState = IIf(.mobile_number(lnCtr).cPostPaid = "1", CheckState.Checked, CheckState.Unchecked)
                                                chk01.Tag = .mobile_number(lnCtr).cPostPaid
                                            Case 2
                                                txtPerso10.Text = .mobile_number(lnCtr).sMobileNo
                                                txtPerso10.Tag = .mobile_number(lnCtr).sMobileNo
                                                txtPerso13.Text = .mobile_number(lnCtr).nPostYear
                                                txtPerso13.Tag = .mobile_number(lnCtr).nPostYear
                                                chk02.CheckState = IIf(.mobile_number(lnCtr).cPostPaid = "1", CheckState.Checked, CheckState.Unchecked)
                                                chk02.Tag = .mobile_number(lnCtr).cPostPaid
                                        End Select
                                    Next
                                Case 14
                                    For lnRow As Integer = 0 To .landline.Count - 1
                                        Select Case lnRow
                                            Case 0
                                                loTxt.Text = .landline(lnRow).sPhoneNox
                                                loTxt.Tag = .landline(lnRow).sPhoneNox
                                            Case 1
                                                txtPerso15.Text = .landline(lnRow).sPhoneNox
                                                txtPerso15.Tag = .landline(lnRow).sPhoneNox
                                            Case 2
                                                txtPerso16.Text = .landline(lnRow).sPhoneNox
                                                txtPerso16.Tag = .landline(lnRow).sPhoneNox
                                        End Select
                                    Next
                                Case 17
                                    For lnRow As Integer = 0 To .email_address.Count - 1
                                        Select Case lnRow
                                            Case 0
                                                loTxt.Text = .email_address(lnRow).sEmailAdd
                                                loTxt.Tag = .email_address(lnRow).sEmailAdd
                                            Case 1
                                                txtPerso18.Text = .email_address(lnRow).sEmailAdd
                                                txtPerso18.Tag = .email_address(lnRow).sEmailAdd
                                            Case 2
                                                txtPerso19.Text = .email_address(lnRow).sEmailAdd
                                                txtPerso19.Tag = .email_address(lnRow).sEmailAdd
                                        End Select
                                    Next
                                Case 20
                                    loTxt.Text = .facebook.sFBAcctxx
                                    loTxt.Tag = .facebook.sFBAcctxx
                                Case 21
                                    loTxt.Text = .sVibeAcct
                                    loTxt.Tag = .sVibeAcct
                                Case 22
                                    loTxt.Text = .sMaidenNm
                                    loTxt.Tag = .sMaidenNm
                                Case 24
                                    loTxt.Text = .facebook.nNoFriend
                                    loTxt.Tag = .facebook.nNoFriend
                                Case 25
                                    loTxt.Text = .facebook.nYearxxxx
                                    loTxt.Tag = .facebook.nYearxxxx
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb2Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.applicant_info, p_oTrans.Category.applicant_info)
                                Select Case loIndex
                                    Case 0
                                        loTxt.Text = IIf(.sLastName = "", "N/A", .sLastName)
                                    Case 1
                                        loTxt.Text = IIf(.sFrstName = "", "N/A", .sFrstName)
                                    Case 2
                                        loTxt.Text = IIf(.sMiddName = "", "N/A", .sMiddName)
                                    Case 3
                                        loTxt.Text = IIf(.sSuffixNm = "", "N/A", .sSuffixNm)
                                    Case 4
                                        loTxt.Text = IIf(.sNickName = "", "N/A", .sNickName)
                                    Case 5
                                        If Not IsDate(.dBirthDte) Then
                                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                        Else
                                            loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                        End If
                                    Case 6
                                        loTxt.Text = IIf(.sBirthPlc = "", "N/A", p_oTrans.getTownCity(.sBirthPlc, False, True, ""))
                                    Case 9
                                        loTxt.Text = IIf(.sCitizenx = "", "N/A", p_oTrans.getCountry(.sCitizenx, False, True, ""))
                                    Case 10
                                        For lnRow As Integer = 0 To .mobile_number.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                    lb2Field07.Text = IIf(.mobile_number(lnRow).nPostYear = "", "N/A", .mobile_number(lnRow).nPostYear)
                                                    lb2Field29.Text = IIf(.mobile_number(lnRow).cPostPaid = "", "N/A", IIf(.mobile_number(lnRow).cPostPaid = "0", "No", "Yes"))
                                                Case 1
                                                    lb2Field11.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                    lb2Field13.Text = IIf(.mobile_number(lnRow).nPostYear = "", "N/A", .mobile_number(lnRow).nPostYear)
                                                    lb2Field30.Text = IIf(.mobile_number(lnRow).cPostPaid = "", "N/A", IIf(.mobile_number(lnRow).cPostPaid = "0", "No", "Yes"))
                                                Case 2
                                                    lb2Field12.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                    lb2Field14.Text = IIf(.mobile_number(lnRow).nPostYear = "", "N/A", .mobile_number(lnRow).nPostYear)
                                                    lb2Field31.Text = IIf(.mobile_number(lnRow).cPostPaid = "", "N/A", IIf(.mobile_number(lnRow).cPostPaid = "0", "No", "Yes"))
                                            End Select
                                        Next
                                    Case 15
                                        For lnRow As Integer = 0 To .landline.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.landline(lnRow).sPhoneNox = "", "N/A", .landline(lnRow).sPhoneNox)
                                                Case 1
                                                    lb2Field16.Text = IIf(.landline(lnRow).sPhoneNox = "", "N/A", .landline(lnRow).sPhoneNox)
                                                Case 2
                                                    lb2Field17.Text = IIf(.landline(lnRow).sPhoneNox = "", "N/A", .landline(lnRow).sPhoneNox)
                                            End Select
                                        Next
                                    Case 20
                                        For lnRow As Integer = 0 To .email_address.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.email_address(lnRow).sEmailAdd = "", "N/A", .email_address(lnRow).sEmailAdd)
                                                Case 1
                                                    lb2Field21.Text = IIf(.email_address(lnRow).sEmailAdd = "", "N/A", .email_address(lnRow).sEmailAdd)
                                                Case 2
                                                    lb2Field22.Text = IIf(.email_address(lnRow).sEmailAdd = "", "N/A", .email_address(lnRow).sEmailAdd)
                                            End Select
                                        Next
                                    Case 23
                                        loTxt.Text = IIf(.facebook.sFBAcctxx = "", "N/A", .facebook.sFBAcctxx)
                                    Case 24
                                        loTxt.Text = IIf(.sVibeAcct = "", "N/A", .sVibeAcct)
                                    Case 25
                                        loTxt.Text = IIf(.sMaidenNm = "", "N/A", .sMaidenNm)
                                    Case 27
                                        loTxt.Text = IIf(.facebook.nNoFriend = "", "N/A", .facebook.nNoFriend)
                                    Case 28
                                        loTxt.Text = IIf(.facebook.nYearxxxx = "", "N/A", .facebook.nYearxxxx)
                                End Select
                                If (.cCvilStat <> "") Then setCivilStat(.cCvilStat, cmb03, lb2Field18)
                                cmb03.Tag = .cCvilStat
                                If (.cGenderCd <> "") Then setGender(.cGenderCd, cmb04, lb2Field19)
                                cmb04.Tag = .cGenderCd
                                If (.facebook.cAcctStat <> "") Then setAccountStatus(.facebook.cAcctStat, cmb62, lb2Field26)
                                cmb62.Tag = .facebook.cAcctStat
                            End With
                        End If
                    End If
                End If
            End If
        Next
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
                        With p_oTrans.Category.other_info
                            Select Case loIndex
                                Case 4
                                    loTxt.Text = .sSrceInfo
                                    loTxt.Tag = .sSrceInfo
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb6Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.other_info, p_oTrans.Category.other_info)
                                Select Case loIndex
                                    Case 5
                                        loTxt.Text = IIf(.sSrceInfo = "", "N/A", .sSrceInfo)
                                End Select
                                If (.sUnitUser <> "") Then setUnitUser(.sUnitUser, cmb26, lb6Field00)
                                cmb26.Tag = .sUnitUser
                                If (.sUsr2Buyr <> "") Then setUserBuyer(.sUsr2Buyr, cmb27, lb6Field01)
                                cmb27.Tag = .sUsr2Buyr
                                If (.sPurposex <> "") Then setPurpose(.sPurposex, cmb28, lb6Field02)
                                cmb28.Tag = .sPurposex
                                If (.sUnitPayr <> "") Then setUnitPayor(.sUnitPayr, cmb29, lb6Field03)
                                cmb29.Tag = .sUnitPayr
                                If (.sPyr2Buyr <> "") Then setUnitPayr2(.sPyr2Buyr, cmb30, lb6Field04)
                                cmb30.Tag = .sPyr2Buyr
                            End With
                        End If
                    End If
                End If
            End If
        Next
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
                        With p_oTrans.Category.disbursement_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nElctrcBl), xsDECIMAL)
                                    loTxt.Tag = .monthly_expenses.nElctrcBl
                                Case 1
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nWaterBil), xsDECIMAL)
                                    loTxt.Tag = .monthly_expenses.nWaterBil
                                Case 2
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nFoodAllw), xsDECIMAL)
                                    loTxt.Tag = .monthly_expenses.nFoodAllw
                                Case 3
                                    loTxt.Text = Format(CDbl(.monthly_expenses.nLoanAmtx), xsDECIMAL)
                                    loTxt.Tag = .monthly_expenses.nLoanAmtx
                                Case 4
                                    loTxt.Text = .properties.sProprty1
                                    loTxt.Tag = .properties.sProprty1
                                Case 5
                                    loTxt.Text = .properties.sProprty2
                                    loTxt.Tag = .properties.sProprty2
                                Case 6
                                    loTxt.Text = .properties.sProprty3
                                    loTxt.Tag = .properties.sProprty3
                                Case 7
                                    loTxt.Text = .bank_account.sBankName
                                    loTxt.Tag = .bank_account.sBankName
                                Case 8
                                    loTxt.Text = .credit_card.sBankName
                                    loTxt.Tag = .credit_card.sBankName
                                Case 9
                                    loTxt.Text = Format(CDbl(.credit_card.nCrdLimit), xsDECIMAL)
                                    loTxt.Tag = .credit_card.nCrdLimit
                                Case 10
                                    loTxt.Text = .credit_card.nSinceYrx
                                    loTxt.Tag = .credit_card.nSinceYrx
                                Case 11
                                    loTxt.Text = .dependent_info.nHouseHld
                                    loTxt.Tag = .dependent_info.nHouseHld
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb5Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.disbursement_info, p_oTrans.Category.disbursement_info)
                                Select Case loIndex
                                    Case 0
                                        loTxt.Text = IIf(.monthly_expenses.nElctrcBl = "", "N/A", Format(CDbl(.monthly_expenses.nElctrcBl), xsDECIMAL))
                                    Case 1
                                        loTxt.Text = IIf(.monthly_expenses.nWaterBil = "", "N/A", Format(CDbl(.monthly_expenses.nWaterBil), xsDECIMAL))
                                    Case 2
                                        loTxt.Text = IIf(.monthly_expenses.nFoodAllw = "", "N/A", Format(CDbl(.monthly_expenses.nFoodAllw), xsDECIMAL))
                                    Case 3
                                        loTxt.Text = IIf(.monthly_expenses.nLoanAmtx = "", "N/A", Format(CDbl(.monthly_expenses.nLoanAmtx), xsDECIMAL))
                                    Case 4
                                        loTxt.Text = IIf(.properties.sProprty1 = "", "N/A", .properties.sProprty1)
                                    Case 5
                                        loTxt.Text = IIf(.properties.sProprty2 = "", "N/A", .properties.sProprty2)
                                    Case 7
                                        loTxt.Text = IIf(.properties.sProprty3 = "", "N/A", .properties.sProprty3)
                                    Case 14
                                        loTxt.Text = IIf(.bank_account.sBankName = "", "N/A", .bank_account.sBankName)
                                    Case 16
                                        loTxt.Text = IIf(.credit_card.sBankName = "", "N/A", .credit_card.sBankName)
                                    Case 17
                                        loTxt.Text = IIf(.credit_card.nCrdLimit = "", "N/A", Format(CDbl(.credit_card.nCrdLimit), xsDECIMAL))
                                    Case 18
                                        loTxt.Text = IIf(.credit_card.nSinceYrx = "", "N/A", .credit_card.nSinceYrx)
                                    Case 19
                                        loTxt.Text = IIf(.dependent_info.nHouseHld = "", "N/A", .dependent_info.nHouseHld)
                                End Select
                                If (.properties.cWith4Whl <> "") Then setIsUniformed(.properties.cWith4Whl, cmb60, lb5Field08)
                                cmb60.Tag = .properties.cWith4Whl
                                If (.properties.cWith3Whl <> "") Then setIsUniformed(.properties.cWith3Whl, cmb19, lb5Field09)
                                cmb19.Tag = .properties.cWith3Whl
                                If (.properties.cWith2Whl <> "") Then setIsUniformed(.properties.cWith2Whl, cmb20, lb5Field10)
                                cmb20.Tag = .properties.cWith2Whl
                                If (.properties.cWithRefx <> "") Then setIsUniformed(.properties.cWithRefx, cmb21, lb5Field11)
                                cmb21.Tag = .properties.cWithRefx
                                If (.properties.cWithTVxx <> "") Then setIsUniformed(.properties.cWithTVxx, cmb22, lb5Field12)
                                cmb22.Tag = .properties.cWithTVxx
                                If (.properties.cWithACxx <> "") Then setIsUniformed(.properties.cWithACxx, cmb23, lb5Field13)
                                cmb23.Tag = .properties.cWithACxx
                                If (.bank_account.sAcctType <> "") Then setBankType(.bank_account.sAcctType, cmb24, lb5Field15)
                                cmb24.Tag = .bank_account.sAcctType
                            End With
                        End If
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtCoMak" Then
                        With p_oTrans.Category.comaker_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName
                                    loTxt.Tag = .sLastName
                                Case 1
                                    loTxt.Text = .sFrstName
                                    loTxt.Tag = .sFrstName
                                Case 2
                                    loTxt.Text = .sMiddName
                                    loTxt.Tag = .sMiddName
                                Case 3
                                    loTxt.Text = .sSuffixNm
                                    loTxt.Tag = .sSuffixNm
                                Case 4
                                    loTxt.Text = .sNickName
                                    loTxt.Tag = .sNickName
                                Case 5
                                    If .dBirthDte = "" Then
                                        loTxt.Text = ""
                                    ElseIf .dBirthDte = "Invalid Date" Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                    End If
                                    loTxt.Tag = .dBirthDte
                                Case 6
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                    loTxt.Tag = .sBirthPlc
                                Case 9
                                    For lnRow As Integer = 0 To .mobile_number.Count - 1
                                        Select Case lnRow
                                            Case 0
                                                loTxt.Text = .mobile_number(lnRow).sMobileNo
                                                loTxt.Tag = .mobile_number(lnRow).sMobileNo
                                            Case 1
                                                txtCoMak11.Text = .mobile_number(lnRow).sMobileNo
                                                txtCoMak11.Tag = .mobile_number(lnRow).sMobileNo
                                            Case 2
                                                txtCoMak12.Text = .mobile_number(lnRow).sMobileNo
                                                txtCoMak12.Tag = .mobile_number(lnRow).sMobileNo
                                        End Select
                                    Next
                                Case 10
                                    loTxt.Text = .sFBAcctxx
                                    loTxt.Tag = .sFBAcctxx
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb9Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.comaker_info, p_oTrans.Category.comaker_info)
                                Select Case loIndex
                                    Case 0
                                        loTxt.Text = IIf(.sLastName = "", "N/A", .sLastName)
                                    Case 1
                                        loTxt.Text = IIf(.sFrstName = "", "N/A", .sFrstName)
                                    Case 2
                                        loTxt.Text = IIf(.sMiddName = "", "N/A", .sMiddName)
                                    Case 3
                                        loTxt.Text = IIf(.sSuffixNm = "", "N/A", .sSuffixNm)
                                    Case 4
                                        loTxt.Text = IIf(.sNickName = "", "N/A", .sNickName)
                                    Case 5
                                        If (Not IsDate(.dBirthDte)) Then
                                            loTxt.Text = ""
                                        Else
                                            loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                        End If
                                    Case 6
                                        loTxt.Text = IIf(.sBirthPlc = "", "N/A", p_oTrans.getTownCity(.sBirthPlc, False, True, ""))
                                    Case 9
                                        For lnRow As Integer = 0 To .mobile_number.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                Case 1
                                                    lb9Field11.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                Case 2
                                                    lb9Field12.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                            End Select
                                        Next
                                    Case 10
                                        loTxt.Text = IIf(.sFBAcctxx = "", "N/A", .sFBAcctxx)
                                End Select
                                If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb70, lb9Field07)
                                cmb70.Tag = .cIncmeSrc
                                If (.sReltnCde <> "") Then setFinanceType(.sReltnCde, cmb71, lb9Field08)
                                cmb71.Tag = .sReltnCde
                            End With
                        End If
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
                        With p_oTrans.Category.spouse_info.personal_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName
                                    loTxt.Tag = .sLastName
                                Case 1
                                    loTxt.Text = .sFrstName
                                    loTxt.Tag = .sFrstName
                                Case 2
                                    loTxt.Text = .sSuffixNm
                                    loTxt.Tag = .sSuffixNm
                                Case 3
                                    loTxt.Text = .sMiddName
                                    loTxt.Tag = .sMiddName
                                Case 4
                                    loTxt.Text = .sNickName
                                    loTxt.Tag = .sNickName
                                Case 5
                                    If .dBirthDte = "" Then
                                        loTxt.Text = ""
                                    ElseIf Not IsDate(.dBirthDte) Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                    End If
                                    loTxt.Tag = .dBirthDte
                                Case 6
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                    loTxt.Tag = .sBirthPlc
                                Case 7
                                    loTxt.Text = p_oTrans.getCountry(.sCitizenx, False, True, "")
                                    loTxt.Tag = .sCitizenx
                                Case 8
                                    For lnRow As Integer = 0 To .mobile_number.Count - 1
                                        Select Case lnRow
                                            Case 0
                                                loTxt.Text = .mobile_number(lnRow).sMobileNo
                                                loTxt.Tag = .mobile_number(lnRow).sMobileNo
                                            Case 1
                                                txtSpoIn09.Text = .mobile_number(lnRow).sMobileNo
                                                txtSpoIn09.Tag = .mobile_number(lnRow).sMobileNo
                                            Case 2
                                                txtSpoIn10.Text = .mobile_number(lnRow).sMobileNo
                                                txtSpoIn10.Tag = .mobile_number(lnRow).sMobileNo
                                        End Select
                                    Next
                                Case 11
                                    For lnRow As Integer = 0 To .landline.Count - 1
                                        Select Case lnRow
                                            Case 0
                                                loTxt.Text = .landline(lnRow).sPhoneNox
                                                loTxt.Tag = .landline(lnRow).sPhoneNox
                                            Case 1
                                                txtSpoIn12.Text = .landline(lnRow).sPhoneNox
                                                txtSpoIn12.Tag = .landline(lnRow).sPhoneNox
                                            Case 2
                                                txtSpoIn13.Text = .landline(lnRow).sPhoneNox
                                                txtSpoIn13.Tag = .landline(lnRow).sPhoneNox
                                        End Select
                                    Next
                                Case 14
                                    loTxt.Text = .sMaidenNm
                                    loTxt.Tag = .sMaidenNm
                                Case 15
                                    For lnRow As Integer = 0 To .email_address.Count - 1
                                        Select Case lnRow
                                            Case 0
                                                loTxt.Text = .email_address(lnRow).sEmailAdd
                                                loTxt.Tag = .email_address(lnRow).sEmailAdd
                                            Case 1
                                                txtSpoIn16.Text = .email_address(lnRow).sEmailAdd
                                                txtSpoIn16.Tag = .email_address(lnRow).sEmailAdd
                                            Case 2
                                                txtSpoIn17.Text = .email_address(lnRow).sEmailAdd
                                                txtSpoIn17.Tag = .email_address(lnRow).sEmailAdd
                                        End Select
                                    Next
                                Case 18
                                    loTxt.Text = .facebook.sFBAcctxx
                                    loTxt.Tag = .facebook.sFBAcctxx
                                Case 20
                                    loTxt.Text = .facebook.cAcctStat
                                    loTxt.Tag = .facebook.cAcctStat
                                Case 21
                                    loTxt.Text = .facebook.nNoFriend
                                    loTxt.Tag = .facebook.nNoFriend
                                Case 22
                                    loTxt.Text = .facebook.nYearxxxx
                                    loTxt.Tag = .facebook.nYearxxxx
                                Case 19
                                    loTxt.Text = .sVibeAcct
                                    loTxt.Tag = .sVibeAcct
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb7Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.spouse_info.personal_info, p_oTrans.Category.spouse_info.personal_info)
                                Select Case loIndex
                                    Case 0
                                        loTxt.Text = IIf(.sLastName = "", "N/A", .sLastName)
                                    Case 1
                                        loTxt.Text = IIf(.sFrstName = "", "N/A", .sFrstName)
                                    Case 2
                                        loTxt.Text = IIf(.sSuffixNm = "", "N/A", .sSuffixNm)
                                    Case 3
                                        loTxt.Text = IIf(.sMiddName = "", "N/A", .sMiddName)
                                    Case 4
                                        loTxt.Text = IIf(.sNickName = "", "N/A", .sNickName)
                                    Case 5
                                        If .dBirthDte = "" Then
                                            loTxt.Text = "N/A"
                                        ElseIf Not IsDate(.dBirthDte) Then
                                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                        Else
                                            loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                        End If
                                    Case 6
                                        loTxt.Text = IIf(.sBirthPlc = "", "N/A", p_oTrans.getTownCity(.sBirthPlc, False, True, ""))
                                    Case 7
                                        loTxt.Text = IIf(.sCitizenx = "", "N/A", p_oTrans.getCountry(.sCitizenx, False, True, ""))
                                    Case 8
                                        For lnRow As Integer = 0 To .mobile_number.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                Case 1
                                                    lb7Field09.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                                Case 2
                                                    lb7Field10.Text = IIf(.mobile_number(lnRow).sMobileNo = "", "N/A", .mobile_number(lnRow).sMobileNo)
                                            End Select
                                        Next
                                    Case 11
                                        For lnRow As Integer = 0 To .landline.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.landline(lnRow).sPhoneNox = "", "N/A", .landline(lnRow).sPhoneNox)
                                                Case 1
                                                    lb7Field12.Text = IIf(.landline(lnRow).sPhoneNox = "", "N/A", .landline(lnRow).sPhoneNox)
                                                Case 2
                                                    lb7Field13.Text = IIf(.landline(lnRow).sPhoneNox = "", "N/A", .landline(lnRow).sPhoneNox)
                                            End Select
                                        Next
                                    Case 16
                                        loTxt.Text = IIf(.sMaidenNm = "", "N/A", .sMaidenNm)
                                    Case 17
                                        For lnRow As Integer = 0 To .email_address.Count - 1
                                            Select Case lnRow
                                                Case 0
                                                    loTxt.Text = IIf(.email_address(lnRow).sEmailAdd = "", "N/A", .email_address(lnRow).sEmailAdd)
                                                Case 1
                                                    lb7Field18.Text = IIf(.email_address(lnRow).sEmailAdd = "", "N/A", .email_address(lnRow).sEmailAdd)
                                                Case 2
                                                    lb7Field19.Text = IIf(.email_address(lnRow).sEmailAdd = "", "N/A", .email_address(lnRow).sEmailAdd)
                                            End Select
                                        Next
                                    Case 20
                                        loTxt.Text = IIf(.facebook.sFBAcctxx = "", "N/A", .facebook.sFBAcctxx)
                                    Case 23
                                        loTxt.Text = IIf(.facebook.nNoFriend = "", "N/A", .facebook.nNoFriend)
                                    Case 24
                                        loTxt.Text = IIf(.facebook.nYearxxxx = "", "N/A", .facebook.nYearxxxx)
                                    Case 21
                                        loTxt.Text = IIf(.sVibeAcct = "", "N/A", .sVibeAcct)
                                End Select
                                If (.cCvilStat <> "") Then setCivilStat(.cCvilStat, cmb33, lb7Field14)
                                cmb33.Tag = .cCvilStat
                                If (.facebook.cAcctStat <> "") Then setAccountStatus(.facebook.cAcctStat, cmb75, lb7Field22)
                                cmb75.Tag = .facebook.cAcctStat
                                If (.cGenderCd <> "") Then setGender(.cGenderCd, cmb34, lb7Field15)
                                cmb34.Tag = .cGenderCd
                            End With
                        End If
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoRe" Then
                        With p_oTrans.Category.spouse_info.residence_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .present_address.sLandMark
                                    loTxt.Tag = .present_address.sLandMark
                                Case 1
                                    loTxt.Text = .present_address.sHouseNox
                                    loTxt.Tag = .present_address.sHouseNox
                                Case 2
                                    loTxt.Text = .present_address.sAddress1
                                    loTxt.Tag = .present_address.sAddress1
                                Case 3
                                    loTxt.Text = .present_address.sAddress2
                                    loTxt.Tag = .present_address.sAddress2
                                Case 4
                                    loTxt.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                    loTxt.Tag = .present_address.sTownIDxx
                                Case 5
                                    loTxt.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                    loTxt.Tag = .present_address.sBrgyIDxx
                                Case 6
                                    If (Not IsNothing(.rent_others)) Then
                                        loTxt.Text = .rent_others.nLenStayx
                                        loTxt.Tag = .rent_others.nLenStayx
                                    End If
                                Case 7
                                    If Not IsNothing(.rent_others) Then
                                        loTxt.Text = .rent_others.nRentExps
                                        loTxt.Tag = .rent_others.nRentExps
                                    End If
                                Case 8
                                    loTxt.Text = .sCtkReltn
                                    loTxt.Tag = .sCtkReltn
                                Case 9
                                    loTxt.Text = .permanent_address.sLandMark
                                    loTxt.Tag = .permanent_address.sLandMark
                                Case 10
                                    loTxt.Text = .permanent_address.sHouseNox
                                    loTxt.Tag = .permanent_address.sHouseNox
                                Case 11
                                    loTxt.Text = .permanent_address.sAddress1
                                    loTxt.Tag = .permanent_address.sAddress1
                                Case 12
                                    loTxt.Text = .permanent_address.sAddress2
                                    loTxt.Tag = .permanent_address.sAddress2
                                Case 13
                                    loTxt.Text = p_oTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                    loTxt.Tag = .permanent_address.sTownIDxx
                                Case 14
                                    loTxt.Text = p_oTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                                    loTxt.Tag = .permanent_address.sBrgyIDxx
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "l10Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.spouse_info.residence_info, p_oTrans.Category.spouse_info.residence_info)
                                Select Case loIndex
                                    Case 0
                                        loTxt.Text = IIf(.present_address.sLandMark = "", "N/A", .present_address.sLandMark)
                                    Case 1
                                        loTxt.Text = IIf(.present_address.sHouseNox = "", "N/A", .present_address.sHouseNox)
                                    Case 2
                                        loTxt.Text = IIf(.present_address.sAddress1 = "", "N/A", .present_address.sAddress1)
                                    Case 3
                                        loTxt.Text = IIf(.present_address.sAddress2 = "", "N/A", .present_address.sAddress2)
                                    Case 4
                                        loTxt.Text = IIf(.present_address.sTownIDxx = "", "N/A", p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, ""))
                                    Case 5
                                        loTxt.Text = IIf(.present_address.sBrgyIDxx = "", "N/A", p_oTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, ""))
                                    Case 8
                                        loTxt.Text = IIf(.rent_others.nLenStayx = "", "N/A", IIf(.rent_others.nLenStayx = "", "", CInt(.rent_others.nLenStayx)))
                                    Case 9
                                        loTxt.Text = IIf(.rent_others.nRentExps = "", "N/A", IIf(.rent_others.nRentExps = "", "", CDbl(.rent_others.nRentExps)))
                                    Case 10
                                        loTxt.Text = IIf(.sCtkReltn = "", "N/A", .sCtkReltn)
                                    Case 13
                                        loTxt.Text = IIf(.permanent_address.sLandMark = "", "N/A", .permanent_address.sLandMark)
                                    Case 14
                                        loTxt.Text = IIf(.permanent_address.sHouseNox = "", "N/A", .permanent_address.sHouseNox)
                                    Case 15
                                        loTxt.Text = IIf(.permanent_address.sAddress1 = "", "N/A", .permanent_address.sAddress1)
                                    Case 16
                                        loTxt.Text = IIf(.permanent_address.sAddress2 = "", "N/A", .permanent_address.sAddress2)
                                    Case 17
                                        loTxt.Text = IIf(.permanent_address.sTownIDxx = "", "N/A", p_oTrans.getTownCity(.permanent_address.sTownIDxx, False, True, ""))
                                    Case 18
                                        loTxt.Text = IIf(.permanent_address.sBrgyIDxx = "", "N/A", p_oTrans.getTownCity(.permanent_address.sBrgyIDxx, False, True, ""))
                                End Select
                                If (.cOwnershp <> "") Then setOwnership(.cOwnershp, cmb80, l10Field06)
                                cmb80.Tag = .cOwnershp
                                If (.cOwnOther <> "") Then setOwnedOther(.cOwnOther, cmb81, l10Field07)
                                cmb81.Tag = .cOwnOther
                                If (.cGaragexx <> "") Then setGarage(.cGaragexx, cmb82, l10Field12)
                                cmb82.Tag = .cGaragexx
                                If (.cHouseTyp <> "") Then setHouseType(.cHouseTyp, cmb83, l10Field11)
                                cmb83.Tag = .cHouseTyp
                                If (.rent_others.cRntOther <> "") Then setRent(.rent_others.cRntOther, cmb84, l10Field19)
                                cmb84.Tag = .rent_others.cRntOther
                            End With
                        End If
                    End If
                End If
            End If
        Next
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
                        With p_oTrans.Category.spouse_means
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .employed.sIndstWrk
                                    loTxt.Tag = .employed.sIndstWrk
                                Case 1
                                    loTxt.Text = .employed.sEmployer
                                    loTxt.Tag = .employed.sEmployer
                                Case 2
                                    loTxt.Text = .employed.sWrkAddrx
                                    loTxt.Tag = .employed.sWrkAddrx
                                Case 3
                                    loTxt.Text = p_oTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                    loTxt.Tag = .employed.sWrkTownx
                                Case 4
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                    loTxt.Tag = .employed.sPosition
                                Case 5
                                    loTxt.Text = .employed.sFunction
                                    loTxt.Tag = .employed.sFunction
                                Case 7
                                    loTxt.Text = .employed.nLenServc
                                    loTxt.Tag = .employed.nLenServc
                                Case 8
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                    loTxt.Tag = .employed.nSalaryxx
                                Case 9
                                    loTxt.Text = .employed.sWrkTelno
                                    loTxt.Tag = .employed.sWrkTelno
                                Case 10
                                    loTxt.Text = .self_employed.sIndstBus
                                    loTxt.Tag = .self_employed.sIndstBus
                                Case 11
                                    loTxt.Text = .self_employed.sBusiness
                                    loTxt.Tag = .self_employed.sBusiness
                                Case 12
                                    loTxt.Text = .self_employed.sBusAddrx
                                    loTxt.Tag = .self_employed.sBusAddrx
                                Case 13
                                    loTxt.Text = p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                    loTxt.Tag = .self_employed.sBusTownx
                                Case 15
                                    loTxt.Text = .self_employed.nBusLenxx
                                    loTxt.Tag = .self_employed.nBusLenxx
                                Case 16
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                                    loTxt.Tag = .self_employed.nBusIncom
                                Case 17
                                    loTxt.Text = Format(CDbl(.self_employed.nMonExpns), xsDECIMAL)
                                    loTxt.Tag = .self_employed.nMonExpns
                                Case 20
                                    loTxt.Text = p_oTrans.getCountry(.employed.sOFWNatnx, False, True, "")
                                    loTxt.Tag = .employed.sOFWNatnx
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb8Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.spouse_means, p_oTrans.Category.spouse_means)
                                Select Case loIndex
                                    Case 9
                                        loTxt.Text = IIf(.employed.sOFWNatnx = "", "N/A", p_oTrans.getCountry(.employed.sOFWNatnx, False, True, ""))
                                    Case 10
                                        loTxt.Text = IIf(.employed.sIndstWrk = "", "N/A", .employed.sIndstWrk)
                                    Case 11
                                        loTxt.Text = IIf(.employed.sEmployer = "", "N/A", .employed.sEmployer)
                                    Case 12
                                        loTxt.Text = IIf(.employed.sWrkAddrx = "", "N/A", .employed.sWrkAddrx)
                                    Case 13
                                        loTxt.Text = IIf(.employed.sWrkTownx = "", "N/A", p_oTrans.getTownCity(.employed.sWrkTownx, False, True, ""))
                                    Case 14
                                        loTxt.Text = IIf(.employed.sPosition = "", "N/A", p_oTrans.getOccupation(.employed.sPosition, False, True, ""))
                                    Case 15
                                        loTxt.Text = IIf(.employed.sFunction = "", "N/A", .employed.sFunction)
                                    Case 17
                                        loTxt.Text = IIf(.employed.nLenServc = "", "N/A", .employed.nLenServc)
                                    Case 18
                                        loTxt.Text = IIf(.employed.nSalaryxx = "", "N/A", .employed.nSalaryxx)
                                    Case 19
                                        loTxt.Text = IIf(.employed.sWrkTelno = "", "N/A", .employed.sWrkTelno)
                                    Case 20
                                        loTxt.Text = IIf(.self_employed.sIndstBus = "", "N/A", .self_employed.sIndstBus)
                                    Case 21
                                        loTxt.Text = IIf(.self_employed.sBusiness = "", "N/A", .self_employed.sBusiness)
                                    Case 22
                                        loTxt.Text = IIf(.self_employed.sBusAddrx = "", "N/A", .self_employed.sBusAddrx)
                                    Case 23
                                        loTxt.Text = IIf(.self_employed.sBusTownx = "", "N/A", p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, ""))
                                    Case 25
                                        loTxt.Text = IIf(.self_employed.nBusLenxx = "", "N/A", .self_employed.nBusLenxx)
                                    Case 26
                                        loTxt.Text = IIf(.self_employed.nBusIncom = "", "N/A", Format(CDbl(.self_employed.nBusIncom), xsDECIMAL))
                                    Case 27
                                        loTxt.Text = IIf(.self_employed.nMonExpns = "", "N/A", Format(CDbl(.self_employed.nMonExpns), xsDECIMAL))
                                End Select
                                If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb35, lb8Field00)
                                cmb35.Tag = .cIncmeSrc
                                If (.employed.cEmpSectr <> "") Then setEmploymentSector(.employed.cEmpSectr, cmb36, lb8Field01)
                                cmb36.Tag = .employed.cEmpSectr
                                If (.employed.cUniforme <> "") Then setIsUniformed(.employed.cUniforme, cmb37, lb8Field02)
                                cmb37.Tag = .employed.cUniforme
                                If (.employed.cMilitary <> "") Then setIsMilitaryUniformed(.employed.cMilitary, cmb38, lb8Field03)
                                cmb38.Tag = .employed.cMilitary
                                If (.employed.cGovtLevl <> "") Then setGovernmentLevel(.employed.cGovtLevl, cmb39, lb8Field04)
                                cmb39.Tag = .employed.cGovtLevl
                                If (.employed.cCompLevl <> "") Then setCompanyLevel(.employed.cCompLevl, cmb40, lb8Field05)
                                cmb40.Tag = .employed.cCompLevl
                                If (.employed.cEmpLevlx <> "") Then setEmploymentLevel(.employed.cEmpLevlx, cmb41, lb8Field06)
                                cmb41.Tag = .employed.cEmpLevlx
                                If (.employed.cOcCatgry <> "") Then setOccptCateg(.employed.cOcCatgry, cmb42, lb8Field07)
                                cmb42.Tag = .employed.cOcCatgry
                                If (.employed.cOFWRegnx <> "") Then setOFReg(.employed.cOFWRegnx, cmb43, lb8Field08)
                                cmb43.Tag = .employed.cOFWRegnx
                                If (.employed.cEmpStatx <> "") Then setStatEmployment(.employed.cEmpStatx, cmb97, lb8Field16)
                                cmb97.Tag = .employed.cEmpStatx
                                If (.self_employed.cBusTypex <> "") Then setBusinessOwnership(.self_employed.cBusTypex, cmb98, lb8Field24)
                                cmb98.Tag = .self_employed.cBusTypex
                                If (.self_employed.cOwnTypex <> "") Then setBusinessOwnership(.self_employed.cOwnTypex, cmb99, lb8Field28)
                                cmb99.Tag = .self_employed.cOwnTypex
                                If (.self_employed.cOwnSizex <> "") Then setBusinessSize(.self_employed.cOwnSizex, cmb89, lb8Field29)
                                cmb89.Tag = .self_employed.cOwnSizex
                            End With
                        End If
                    End If
                End If
            End If
        Next
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
                        With p_oTrans.Category.residence_info
                            Select Case loIndex
                                Case 1
                                    loTxt.Text = .present_address.sLandMark
                                    loTxt.Tag = .present_address.sLandMark
                                Case 2
                                    loTxt.Text = .present_address.sHouseNox
                                    loTxt.Tag = .present_address.sHouseNox
                                Case 3
                                    loTxt.Text = .present_address.sAddress1
                                    loTxt.Tag = .present_address.sAddress1
                                Case 4
                                    loTxt.Text = .present_address.sAddress2
                                    loTxt.Tag = .present_address.sAddress2
                                Case 5
                                    loTxt.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                    loTxt.Tag = .present_address.sTownIDxx
                                Case 6
                                    loTxt.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                    loTxt.Tag = .present_address.sBrgyIDxx
                                Case 9
                                    loTxt.Text = IIf(.rent_others.nLenStayx = "", "", CInt(.rent_others.nLenStayx))
                                    loTxt.Tag = .rent_others.nLenStayx
                                Case 10
                                    loTxt.Text = IIf(.rent_others.nRentExps = "", "", CDbl(.rent_others.nRentExps))
                                    loTxt.Tag = .rent_others.nRentExps
                                Case 11
                                    loTxt.Text = .sCtkReltn
                                    loTxt.Tag = .sCtkReltn
                                Case 12
                                    loTxt.Text = .permanent_address.sLandMark
                                    loTxt.Tag = .permanent_address.sLandMark
                                Case 13
                                    loTxt.Text = .permanent_address.sHouseNox
                                    loTxt.Tag = .permanent_address.sHouseNox
                                Case 14
                                    loTxt.Text = .permanent_address.sAddress1
                                    loTxt.Tag = .permanent_address.sAddress1
                                Case 15
                                    loTxt.Text = .permanent_address.sAddress2
                                    loTxt.Tag = .permanent_address.sAddress2
                                Case 16
                                    loTxt.Text = p_oTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                    loTxt.Tag = .permanent_address.sTownIDxx
                                Case 17
                                    loTxt.Text = p_oTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                                    loTxt.Tag = .permanent_address.sBrgyIDxx
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb3Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.residence_info, p_oTrans.Category.residence_info)
                                Select Case loIndex
                                    Case 1
                                        loTxt.Text = IIf(.present_address.sLandMark = "", "N/A", .present_address.sLandMark)
                                    Case 2
                                        loTxt.Text = IIf(.present_address.sHouseNox = "", "N/A", .present_address.sHouseNox)
                                    Case 3
                                        loTxt.Text = IIf(.present_address.sAddress1 = "", "N/A", .present_address.sAddress1)
                                    Case 4
                                        loTxt.Text = IIf(.present_address.sAddress2 = "", "N/A", .present_address.sAddress2)
                                    Case 5
                                        loTxt.Text = IIf(.present_address.sTownIDxx = "", "N/A", p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, ""))
                                    Case 6
                                        loTxt.Text = IIf(.present_address.sBrgyIDxx = "", "N/A", p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, ""))
                                    Case 10
                                        loTxt.Text = IIf(.rent_others.nLenStayx = "", "N/A", CInt(.rent_others.nLenStayx))
                                    Case 11
                                        loTxt.Text = IIf(.rent_others.nRentExps = "", "N/A", CDbl(.rent_others.nRentExps))
                                    Case 12
                                        loTxt.Text = IIf(.sCtkReltn = "", "N/A", .sCtkReltn)
                                    Case 15
                                        loTxt.Text = IIf(.permanent_address.sLandMark = "", "N/A", .permanent_address.sLandMark)
                                    Case 16
                                        loTxt.Text = IIf(.permanent_address.sHouseNox = "", "N/A", .permanent_address.sHouseNox)
                                    Case 17
                                        loTxt.Text = IIf(.permanent_address.sAddress1 = "", "N/A", .permanent_address.sAddress1)
                                    Case 18
                                        loTxt.Text = IIf(.permanent_address.sAddress2 = "", "N/A", .permanent_address.sAddress2)
                                    Case 19
                                        loTxt.Text = IIf(.permanent_address.sTownIDxx = "", "N/A", p_oTrans.getTownCity(.permanent_address.sTownIDxx, False, True, ""))
                                    Case 20
                                        loTxt.Text = IIf(.permanent_address.sBrgyIDxx = "", "N/A", p_oTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, ""))
                                End Select
                                If (.cOwnershp <> "") Then setOwnership(.cOwnershp, cmb90, lb3Field07)
                                cmb90.Tag = .cOwnershp
                                If (.cOwnOther <> "") Then setOwnedOther(.cOwnOther, cmb05, lb3Field08)
                                cmb05.Tag = .cOwnOther
                                If (.rent_others.cRntOther <> "") Then setRent(.rent_others.cRntOther, cmb91, lb3Field09)
                                cmb91.Tag = .rent_others.cRntOther
                                If (.cHouseTyp <> "") Then setHouseType(.cHouseTyp, cmb07, lb3Field13)
                                cmb07.Tag = .cHouseTyp
                                If (.cGaragexx <> "") Then setGarage(.cGaragexx, cmb06, lb3Field14)
                                cmb06.Tag = .cGaragexx
                            End With
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadComakRes(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadComakRes(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtCoAdd" Then
                        With p_oTrans.Category.comaker_info.residence_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .present_address.sLandMark
                                    loTxt.Tag = .present_address.sLandMark
                                Case 1
                                    loTxt.Text = .present_address.sHouseNox
                                    loTxt.Tag = .present_address.sHouseNox
                                Case 2
                                    loTxt.Text = .present_address.sAddress1
                                    loTxt.Tag = .present_address.sAddress1
                                Case 3
                                    loTxt.Text = .present_address.sAddress2
                                    loTxt.Tag = .present_address.sAddress2
                                Case 4
                                    loTxt.Text = p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                                    loTxt.Tag = .present_address.sTownIDxx
                                Case 5
                                    loTxt.Text = p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                                    loTxt.Tag = .present_address.sBrgyIDxx
                                Case 6
                                    loTxt.Text = IIf(.rent_others.nLenStayx = "", "", CInt(.rent_others.nLenStayx))
                                    loTxt.Tag = .rent_others.nLenStayx
                                Case 7
                                    loTxt.Text = IIf(.rent_others.nRentExps = "", "", CDbl(.rent_others.nRentExps))
                                    loTxt.Tag = .rent_others.nRentExps
                                Case 8
                                    loTxt.Text = .sCtkReltn
                                    loTxt.Tag = .sCtkReltn
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lblAddrs" Then
                            With IIf(psButton = 0, p_oTrans.Detail.comaker_info.residence_info, p_oTrans.Category.comaker_info.residence_info)
                                Select Case loIndex
                                    Case 0
                                        loTxt.Text = IIf(.present_address.sLandMark = "", "N/A", .present_address.sLandMark)
                                    Case 1
                                        loTxt.Text = IIf(.present_address.sHouseNox = "", "N/A", .present_address.sHouseNox)
                                    Case 2
                                        loTxt.Text = IIf(.present_address.sAddress1 = "", "N/A", .present_address.sAddress1)
                                    Case 3
                                        loTxt.Text = IIf(.present_address.sAddress2 = "", "N/A", .present_address.sAddress2)
                                    Case 4
                                        loTxt.Text = IIf(.present_address.sTownIDxx = "", "N/A", p_oTrans.getTownCity(.present_address.sTownIDxx, False, True, ""))
                                    Case 5
                                        loTxt.Text = IIf(.present_address.sBrgyIDxx = "", "N/A", p_oTrans.getBarangay(.present_address.sBrgyIDxx, False, True, ""))
                                    Case 9
                                        loTxt.Text = IIf(.rent_others.nLenStayx = "", "N/A", CInt(.rent_others.nLenStayx))
                                    Case 10
                                        loTxt.Text = IIf(.rent_others.nRentExps = "", "N/A", CDbl(.rent_others.nRentExps))
                                    Case 11
                                        loTxt.Text = IIf(.sCtkReltn = "", "N/A", .sCtkReltn)
                                End Select
                                If (.cOwnershp <> "") Then setOwnership(.cOwnershp, cmb54, lblAddrs06)
                                cmb54.Tag = .cOwnershp
                                If (.cOwnOther <> "") Then setOwnedOther(.cOwnOther, cmb55, lblAddrs07)
                                cmb55.Tag = .cOwnOther
                                If (.rent_others.cRntOther <> "") Then setRent(.rent_others.cRntOther, cmb56, lblAddrs08)
                                cmb56.Tag = .rent_others.cRntOther
                                If (.cHouseTyp <> "") Then setHouseType(.cHouseTyp, cmb57, lblAddrs12)
                                cmb57.Tag = .cHouseTyp
                                If (.cGaragexx <> "") Then setGarage(.cGaragexx, cmb58, lblAddrs13)
                                cmb58.Tag = .cGaragexx
                            End With
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub txtComakRes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans.Category.comaker_info.residence_info
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtCoAdd" And loTxt.ReadOnly = False Then
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
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .rent_others.nRentExps = CDbl(loTxt.Text)
                    Case 8
                        .sCtkReltn = loTxt.Text
                End Select
            End If
        End With
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
                        With p_oTrans.Category.means_info
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = p_oTrans.getCountry(.employed.sOFWNatnx, False, True, "")
                                    loTxt.Tag = .employed.sOFWNatnx
                                Case 1
                                    loTxt.Text = .employed.sIndstWrk
                                    loTxt.Tag = .employed.sIndstWrk
                                Case 2
                                    loTxt.Text = .employed.sEmployer
                                    loTxt.Tag = .employed.sEmployer
                                Case 3
                                    loTxt.Text = .employed.sWrkAddrx
                                    loTxt.Tag = .employed.sWrkAddrx
                                Case 4
                                    loTxt.Text = p_oTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                    loTxt.Tag = .employed.sWrkTownx
                                Case 5
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                    loTxt.Tag = .employed.sPosition
                                Case 6
                                    loTxt.Text = .employed.sFunction
                                    loTxt.Tag = .employed.sFunction
                                Case 8
                                    loTxt.Text = .employed.nLenServc
                                    loTxt.Tag = .employed.nLenServc
                                Case 9
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                    loTxt.Tag = .employed.nSalaryxx
                                Case 10
                                    loTxt.Text = .employed.sWrkTelno
                                    loTxt.Tag = .employed.sWrkTelno
                                Case 11
                                    loTxt.Text = .self_employed.sIndstBus
                                    loTxt.Tag = .self_employed.sIndstBus
                                Case 12
                                    loTxt.Text = .self_employed.sBusiness
                                    loTxt.Tag = .self_employed.sBusiness
                                Case 13
                                    loTxt.Text = .self_employed.sBusAddrx
                                    loTxt.Tag = .self_employed.sBusAddrx
                                Case 14
                                    loTxt.Text = p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                    loTxt.Tag = .self_employed.sBusTownx
                                Case 15
                                    loTxt.Text = .self_employed.nBusLenxx
                                    loTxt.Tag = .self_employed.nBusLenxx
                                Case 16
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                                    loTxt.Tag = .self_employed.nBusIncom
                                Case 17
                                    loTxt.Text = Format(CDbl(.self_employed.nMonExpns), xsDECIMAL)
                                    loTxt.Tag = .self_employed.nMonExpns
                                Case 18
                                    loTxt.Text = .financed.sFinancer
                                    loTxt.Tag = .financed.sFinancer
                                Case 19
                                    loTxt.Text = Format(CDbl(.financed.nEstIncme), xsDECIMAL)
                                    loTxt.Tag = .financed.nEstIncme
                                Case 20
                                    loTxt.Text = p_oTrans.getCountry(.financed.sNatnCode, False, True, "")
                                    loTxt.Tag = .financed.sNatnCode
                                Case 21
                                    loTxt.Text = .financed.sMobileNo
                                    loTxt.Tag = .financed.sMobileNo
                                Case 22
                                    loTxt.Text = .financed.sFBAcctxx
                                    loTxt.Tag = .financed.sFBAcctxx
                                Case 23
                                    loTxt.Text = .financed.sEmailAdd
                                    loTxt.Tag = .financed.sEmailAdd
                                Case 24
                                    loTxt.Text = Format(CDbl(.pensioner.nPensionx), xsDECIMAL)
                                    loTxt.Tag = .pensioner.nPensionx
                                Case 25
                                    If Not IsNumeric(.other_income.nOthrIncm) Then
                                        loTxt.Text = CDbl(0)
                                    Else
                                        loTxt.Text = CInt(.pensioner.nRetrYear)
                                    End If
                                    loTxt.Tag = CInt(.pensioner.nRetrYear)
                                Case 26
                                    loTxt.Text = .other_income.sOthrIncm
                                    loTxt.Tag = .other_income.sOthrIncm
                                Case 27
                                    If Not IsNumeric(.other_income.nOthrIncm) Then
                                        loTxt.Text = CDbl(0)
                                    Else
                                        loTxt.Text = Format(CDbl(.other_income.nOthrIncm), xsDECIMAL)
                                    End If
                                    loTxt.Tag = .other_income.nOthrIncm
                            End Select
                        End With
                    End If
                Else
                    If (TypeOf loTxt Is Label) Then
                        Dim loIndex As Integer
                        loIndex = Val(Mid(loTxt.Name, 9))
                        If LCase(Mid(loTxt.Name, 1, 8)) = "lb4Field" Then
                            With IIf(psButton = 0, p_oTrans.Detail.means_info, p_oTrans.Category.means_info)
                                Select Case loIndex
                                    Case 9
                                        loTxt.Text = IIf(.employed.sOFWNatnx = "", "N/A", .employed.sOFWNatnx)
                                    Case 10
                                        loTxt.Text = IIf(.employed.sIndstWrk = "", "N/A", .employed.sIndstWrk)
                                    Case 11
                                        loTxt.Text = IIf(.employed.sEmployer = "", "N/A", .employed.sEmployer)
                                    Case 12
                                        loTxt.Text = IIf(.employed.sWrkAddrx = "", "N/A", .employed.sWrkAddrx)
                                    Case 13
                                        loTxt.Text = IIf(.employed.sWrkTownx = "", "N/A", p_oTrans.getTownCity(.employed.sWrkTownx, False, True, ""))
                                    Case 14
                                        loTxt.Text = IIf(.employed.sPosition = "", "N/A", p_oTrans.getOccupation(.employed.sPosition, False, True, ""))
                                    Case 15
                                        loTxt.Text = IIf(.employed.sFunction = "", "N/A", .employed.sFunction)
                                    Case 17
                                        loTxt.Text = IIf(.employed.nLenServc = "", "N/A", .employed.nLenServc)
                                    Case 18
                                        loTxt.Text = IIf(.employed.nSalaryxx = "", "N/A", .employed.nSalaryxx)
                                    Case 19
                                        loTxt.Text = IIf(.employed.sWrkTelno = "", "N/A", .employed.sWrkTelno)
                                    Case 20
                                        loTxt.Text = IIf(.self_employed.sIndstBus = "", "N/A", .self_employed.sIndstBus)
                                    Case 21
                                        loTxt.Text = IIf(.self_employed.sBusiness = "", "N/A", .self_employed.sBusiness)
                                    Case 22
                                        loTxt.Text = IIf(.self_employed.sBusAddrx = "", "N/A", .self_employed.sBusAddrx)
                                    Case 23
                                        loTxt.Text = IIf(.self_employed.sBusTownx = "", "N/A", p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, ""))
                                    Case 24
                                        loTxt.Text = IIf(.self_employed.nBusLenxx = "", "N/A", .self_employed.nBusLenxx)
                                    Case 26
                                        loTxt.Text = IIf(.self_employed.nBusIncom = "", "N/A", .self_employed.nBusIncom)
                                    Case 27
                                        loTxt.Text = IIf(.self_employed.nMonExpns = "", "N/A", .self_employed.nMonExpns)
                                    Case 31
                                        loTxt.Text = IIf(.financed.sFinancer = "", "N/A", .financed.sFinancer)
                                    Case 32
                                        loTxt.Text = IIf(.financed.nEstIncme = "", "N/A", .financed.nEstIncme)
                                    Case 33
                                        loTxt.Text = IIf(.financed.sNatnCode = "", "N/A", p_oTrans.getCountry(.financed.sNatnCode, False, True, ""))
                                    Case 34
                                        loTxt.Text = IIf(.financed.sMobileNo = "", "N/A", .financed.sMobileNo)
                                    Case 35
                                        loTxt.Text = IIf(.financed.sFBAcctxx = "", "N/A", .financed.sFBAcctxx)
                                    Case 36
                                        loTxt.Text = IIf(.financed.sEmailAdd = "", "N/A", .financed.sEmailAdd)
                                    Case 38
                                        loTxt.Text = IIf(.pensioner.nPensionx = "", "N/A", .pensioner.nPensionx)
                                    Case 39
                                        loTxt.Text = IIf(.pensioner.nRetrYear = "", "N/A", .pensioner.nRetrYear)
                                    Case 40
                                        loTxt.Text = IIf(.other_income.sOthrIncm = "", "N/A", .other_income.sOthrIncm)
                                    Case 41
                                        loTxt.Text = IIf(.other_income.nOthrIncm = "", "N/A", .other_income.sOthrIncm)
                                End Select
                                If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb08, lb4Field00)
                                cmb08.Tag = .cIncmeSrc
                                If (.employed.cEmpSectr <> "") Then setEmploymentSector(.employed.cEmpSectr, cmb09, lb4Field01)
                                cmb09.Tag = .employed.cEmpSectr
                                If (.employed.cUniforme <> "") Then setIsUniformed(.employed.cUniforme, cmb10, lb4Field02)
                                cmb10.Tag = .employed.cUniforme
                                If (.employed.cMilitary <> "") Then setIsMilitaryUniformed(.employed.cMilitary, cmb11, lb4Field03)
                                cmb11.Tag = .employed.cMilitary
                                If (.employed.cGovtLevl <> "") Then setGovernmentLevel(.employed.cGovtLevl, cmb12, lb4Field04)
                                cmb12.Tag = .employed.cGovtLevl
                                If (.employed.cCompLevl <> "") Then setCompanyLevel(.employed.cCompLevl, cmb13, lb4Field05)
                                cmb13.Tag = .employed.cCompLevl
                                If (.employed.cEmpLevlx <> "") Then setEmploymentLevel(.employed.cEmpLevlx, cmb14, lb4Field06)
                                cmb14.Tag = .employed.cEmpLevlx
                                If (.employed.cOcCatgry <> "") Then setOccptCateg(.employed.cOcCatgry, cmb15, lb4Field07)
                                cmb15.Tag = .employed.cOcCatgry
                                If (.employed.cOFWRegnx <> "") Then setOFReg(.employed.cOFWRegnx, cmb16, lb4Field08)
                                cmb16.Tag = .employed.cOFWRegnx
                                If (.employed.cEmpStatx <> "") Then setStatEmployment(.employed.cEmpStatx, cmb96, lb4Field16)
                                cmb96.Tag = .employed.cEmpStatx
                                If (.self_employed.cBusTypex <> "") Then setBusinessOwnership(.self_employed.cBusTypex, cmb17, lb4Field25)
                                cmb17.Tag = .self_employed.cBusTypex
                                If (.self_employed.cOwnTypex <> "") Then setBusinessOwnership(.self_employed.cOwnTypex, cmb18, lb4Field28)
                                cmb18.Tag = .self_employed.cOwnTypex
                                If (.self_employed.cOwnSizex <> "") Then setBusinessSize(.self_employed.cOwnSizex, cmb95, lb4Field29)
                                cmb95.Tag = .self_employed.cOwnSizex
                                If (.financed.sReltnCde <> "") Then setFinanceType(.financed.sReltnCde, cmb85, lb4Field30)
                                cmb85.Tag = .financed.sReltnCde
                                If (.pensioner.cPenTypex <> "") Then setPensionType(.pensioner.cPenTypex, cmb86, lb4Field37)
                                cmb86.Tag = .pensioner.cPenTypex
                            End With
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Public Sub loadDependent_Info()
        With dgv00
            .Rows.Clear()
            If p_oTrans.Category.disbursement_info.dependent_info.children.Count = 0 Then
                p_oTrans.Category.disbursement_info.dependent_info.children.Add(New GOCASConst.children_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Category.disbursement_info.dependent_info.children.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Category.disbursement_info.dependent_info.children(lnCtr).sFullName
                Select Case p_oTrans.Category.disbursement_info.dependent_info.children(lnCtr).sRelatnCD
                    Case 0
                        .Rows(lnCtr).Cells(2).Value = "Children"
                    Case 1
                        .Rows(lnCtr).Cells(2).Value = "Parents"
                    Case 2
                        .Rows(lnCtr).Cells(2).Value = "Siblings"
                    Case 3
                        .Rows(lnCtr).Cells(2).Value = "Relatives"
                    Case 4
                        .Rows(lnCtr).Cells(2).Value = "Others"
                End Select
                .Rows(lnCtr).Cells(3).Value = p_oTrans.Category.disbursement_info.dependent_info.children(lnCtr).nDepdAgex
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv00.CurrentCell = dgv00.Rows(dgv00.RowCount - 1).Cells(0)
        dgv00.Rows(dgv00.RowCount - 1).Selected = True
    End Sub

    Public Sub showReference()
        If p_oTrans.Category.other_info.personal_reference.Count = 0 Then Exit Sub
        With p_oTrans.Category.other_info
            txtOther00.Text = .personal_reference(pdRow).sRefrNmex
            txtOther00.Tag = .personal_reference(pdRow).sRefrNmex
            txtOther01.Text = .personal_reference(pdRow).sRefrMPNx
            txtOther01.Tag = .personal_reference(pdRow).sRefrMPNx
            txtOther02.Text = .personal_reference(pdRow).sRefrAddx
            txtOther02.Tag = .personal_reference(pdRow).sRefrAddx
            txtOther03.Text = p_oTrans.getTownCity(.personal_reference(pdRow).sRefrTown, False, True, "")
            txtOther03.Tag = .personal_reference(pdRow).sRefrTown

            lb6Field06.Text = IIf(.personal_reference(pdRow).sRefrNmex = "", "N/A", .personal_reference(pdRow).sRefrNmex)
            lb6Field07.Text = IIf(.personal_reference(pdRow).sRefrMPNx = "", "N/A", .personal_reference(pdRow).sRefrMPNx)
            lb6Field08.Text = IIf(.personal_reference(pdRow).sRefrAddx = "", "N/A", .personal_reference(pdRow).sRefrAddx)
            lb6Field09.Text = IIf(.personal_reference(pdRow).sRefrTown = "", "N/A", p_oTrans.getTownCity(.personal_reference(pdRow).sRefrTown, False, True, ""))
        End With
    End Sub

    Private Sub loadReference_Info()
        With dgv01
            .Rows.Clear()
            If p_oTrans.Category.other_info.personal_reference.Count = 0 Then
                p_oTrans.Category.other_info.personal_reference.Add(New GOCASConst.personal_reference_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < p_oTrans.Category.other_info.personal_reference.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = p_oTrans.Category.other_info.personal_reference(lnCtr).sRefrNmex
                .Rows(lnCtr).Cells(2).Value = p_oTrans.Category.other_info.personal_reference(lnCtr).sRefrMPNx
                .Rows(lnCtr).Cells(3).Value = p_oTrans.Category.other_info.personal_reference(lnCtr).sRefrAddx
                .Rows(lnCtr).Cells(4).Value = p_oTrans.getTownCity(p_oTrans.Category.other_info.personal_reference(lnCtr).sRefrTown, False, True, "")
                lnCtr = lnCtr + 1
            Loop
        End With
        dgv01.CurrentCell = dgv01.Rows(dgv01.RowCount - 1).Cells(0)
        dgv01.Rows(dgv01.RowCount - 1).Selected = True
    End Sub

    Private Sub dgv00_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv00.Click
        pnRow = dgv00.CurrentRow.Index
        Call showDependentInfo()
    End Sub

    Private Sub showDependentInfo()
        If p_oTrans.Category.disbursement_info.dependent_info.children.Count = 0 Then Exit Sub
        With p_oTrans.Category.disbursement_info.dependent_info
            If .children(pnRow).sFullName <> "" Then
                txtDisbu12.Text = .children(pnRow).sFullName
                txtDisbu12.Tag = .children(pnRow).sFullName

                txtDisbu13.Text = .children(pnRow).nDepdAgex
                txtDisbu13.Tag = .children(pnRow).nDepdAgex

                txtDisbu14.Text = .children(pnRow).sSchlName
                txtDisbu14.Tag = .children(pnRow).sSchlName

                txtDisbu15.Text = .children(pnRow).sSchlAddr
                txtDisbu15.Tag = .children(pnRow).sSchlAddr

                txtDisbu16.Text = p_oTrans.getTownCity(.children(pnRow).sSchlTown, False, True, "")
                txtDisbu16.Tag = .children(pnRow).sSchlTown

                txtDisbu17.Text = .children(pnRow).sCompanyx
                txtDisbu17.Tag = .children(pnRow).sCompanyx

                lb5Field20.Text = .children(pnRow).sFullName
                lb5Field22.Text = .children(pnRow).nDepdAgex
                lb5Field24.Text = .children(pnRow).sSchlName
                lb5Field25.Text = .children(pnRow).sSchlAddr
                lb5Field26.Text = p_oTrans.getTownCity(.children(pnRow).sSchlTown, False, True, "")
                lb5Field31.Text = .children(pnRow).sCompanyx

                setRel(.children(pnRow).sRelatnCD, cmb63, lb5Field21)
                cmb63.Tag = .children(pnRow).sRelatnCD

                setIsStudent(.children(pnRow).cIsPupilx, cmb64, lb5Field23)
                cmb64.Tag = .children(pnRow).cIsPupilx

                setIsPrivate(.children(pnRow).cIsPrivte, cmb65, lb5Field27)
                cmb65.Tag = .children(pnRow).cIsPrivte

                setEducLevel(.children(pnRow).sEducLevl, cmb66, lb5Field28)
                cmb66.Tag = .children(pnRow).sEducLevl

                setIsScholar(.children(pnRow).cIsSchlrx, cmb67, lb5Field29)
                cmb67.Tag = .children(pnRow).cIsSchlrx

                setHasWorked(.children(pnRow).cHasWorkx, cmb68, lb5Field30)
                cmb68.Tag = .children(pnRow).cHasWorkx

                setEmpSector(.children(pnRow).cWorkType, cmb69, lb5Field32)
                cmb69.Tag = .children(pnRow).cWorkType

                setIsHousehold(.children(pnRow).cHouseHld, cmb50, lb5Field33)
                cmb50.Tag = .children(pnRow).cHouseHld

                setIsDependent(.children(pnRow).cDependnt, cmb51, lb5Field34)
                cmb51.Tag = .children(pnRow).cDependnt

                setIsChild(.children(pnRow).cIsChildx, cmb52, lb5Field35)
                cmb52.Tag = .children(pnRow).cIsChildx

                setIsMarried(.children(pnRow).cIsMarrdx, cmb53, lb5Field36)
                cmb53.Tag = .children(pnRow).cIsMarrdx
            Else
                txtDisbu12.Text = ""
                txtDisbu12.Tag = ""
                txtDisbu13.Text = ""
                txtDisbu13.Tag = ""
                txtDisbu14.Text = ""
                txtDisbu14.Tag = ""
                txtDisbu15.Text = ""
                txtDisbu15.Tag = ""
                txtDisbu16.Text = ""
                txtDisbu16.Tag = ""
                txtDisbu17.Text = ""
                txtDisbu17.Tag = ""
                lb5Field20.Text = ""
                lb5Field22.Text = ""
                lb5Field24.Text = ""
                lb5Field25.Text = ""
                lb5Field26.Text = ""
                lb5Field31.Text = ""
                cmb63.SelectedIndex = -1
                cmb63.Tag = ""
                cmb64.SelectedIndex = -1
                cmb64.Tag = ""
                cmb65.SelectedIndex = -1
                cmb65.Tag = ""
                cmb66.SelectedIndex = -1
                cmb66.Tag = ""
                cmb67.SelectedIndex = -1
                cmb67.Tag = ""
                cmb68.SelectedIndex = -1
                cmb68.Tag = ""
                cmb69.SelectedIndex = -1
                cmb69.Tag = ""
                cmb50.SelectedIndex = -1
                cmb50.Tag = ""
                cmb51.SelectedIndex = -1
                cmb51.Tag = ""
                cmb52.SelectedIndex = -1
                cmb52.Tag = ""
                cmb53.SelectedIndex = -1
                cmb53.Tag = ""

                lb5Field21.Text = ""
                lb5Field23.Text = ""
                lb5Field27.Text = ""
                lb5Field28.Text = ""
                lb5Field29.Text = ""
                lb5Field30.Text = ""
                lb5Field32.Text = ""
                lb5Field33.Text = ""
                lb5Field34.Text = ""
                lb5Field35.Text = ""
                lb5Field36.Text = ""
            End If
        End With
    End Sub

    Private Sub clearDependent()
        With p_oTrans.Category.disbursement_info.dependent_info
            cmb64.SelectedIndex = -1
            cmb64.Tag = ""
            cmb65.SelectedIndex = -1
            cmb65.Tag = ""
            cmb67.SelectedIndex = -1
            cmb67.Tag = ""
            cmb68.SelectedIndex = -1
            cmb68.Tag = ""
            cmb69.SelectedIndex = -1
            cmb69.Tag = ""
            cmb50.SelectedIndex = -1
            cmb50.Tag = ""
            cmb51.SelectedIndex = -1
            cmb51.Tag = ""
            cmb52.SelectedIndex = -1
            cmb52.Tag = ""
            cmb53.SelectedIndex = -1
            cmb53.Tag = ""
            cmb66.SelectedIndex = -1
            cmb66.Tag = ""
            cmb63.SelectedIndex = -1
            cmb63.Tag = ""
        End With
    End Sub

    Private Sub dgv01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv01.Click
        pdRow = dgv01.CurrentRow.Index
        Call showReference()
    End Sub

    Private Function DataComplete() As Boolean
        With p_oTrans.Category
            If .nDownPaym = 0 Then
                MessageBox.Show("No Downpayment entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 0
                txtIntro01.Focus()
                Return False
            ElseIf .cApplType = "" Then
                MessageBox.Show("No Application Type Entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 0
                cmb00.Focus()
                Return False
            ElseIf .cUnitAppl = "" Then
                MessageBox.Show("No Application for entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 0
                cmb01.Focus()
                Return False
            ElseIf .applicant_info.sLastName = "" Then
                MessageBox.Show("No Applicant LastName entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                txtPerso00.Focus()
                Return False
            ElseIf .applicant_info.sFrstName = "" Then
                MessageBox.Show("No Applicant Firstname entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                txtPerso01.Focus()
                Return False
            ElseIf .applicant_info.sMiddName = "" Then
                MessageBox.Show("No Applicant Middlename entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                txtPerso02.Focus()
                Return False
            ElseIf Not IsDate(.applicant_info.dBirthDte) Then
                MessageBox.Show("Invalid Birth Date entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                txtPerso05.Focus()
                Return False
            ElseIf .applicant_info.sBirthPlc = "" Then
                MessageBox.Show("No Birth Place entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                txtPerso06.Focus()
                Return False
            ElseIf .applicant_info.sCitizenx = "" Then
                MessageBox.Show("No Citizenship entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                txtPerso07.Focus()
                Return False
            ElseIf .applicant_info.cCvilStat = "" Then
                MessageBox.Show("No Civil Status entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                cmb03.Focus()
                Return False
            ElseIf .applicant_info.cGenderCd = "" Then
                MessageBox.Show("No Gender entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 1
                cmb04.Focus()
                Return False
            ElseIf .residence_info.cOwnershp = "" Then
                MessageBox.Show("No Home ownership entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                cmb90.Focus()
                Return False
            ElseIf .residence_info.present_address.sAddress1 = "" And .residence_info.present_address.sAddress2 = "" Then
                MessageBox.Show("No present Phase #/Lot #/Sitio or Street name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                txtResid03.Focus()
                Return False
            ElseIf .residence_info.present_address.sTownIDxx = "" Then
                MessageBox.Show("No present Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                txtResid05.Focus()
                Return False
            ElseIf .residence_info.present_address.sBrgyIDxx = "" Then
                MessageBox.Show("No present barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                txtResid06.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sAddress1 = "" And .residence_info.permanent_address.sAddress2 = "" Then
                MessageBox.Show("No permanent Phase #/Lot #/Sitio or Street name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                txtResid14.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sTownIDxx = "" Then
                MessageBox.Show("No permanent Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                txtResid16.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sBrgyIDxx = "" Then
                MessageBox.Show("No permanent barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                txtResid17.Focus()
                Return False
            ElseIf .residence_info.cHouseTyp = "" Then
                MessageBox.Show("Invalid House Type entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                cmb07.Focus()
                Return False
            ElseIf .residence_info.cGaragexx = "" Then
                MessageBox.Show("Invalid Garage entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                tabControl00.SelectedIndex = 2
                cmb06.Focus()
                Return False
            ElseIf .applicant_info.mobile_number.Count > 0 Then
                For lnCtr As Integer = 0 To .applicant_info.mobile_number.Count - 1
                    Select Case lnCtr
                        Case 0
                            If .applicant_info.mobile_number(lnCtr).sMobileNo = "" Then
                                MessageBox.Show("No mobile number entry detected...", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                If Not IsNothing(.spouse_info) Then
                    If .spouse_info.personal_info.sLastName = "" Then
                        MessageBox.Show("No Spouse Lastname entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn00.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sFrstName = "" Then
                        MessageBox.Show("No Spouse Firstname entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn01.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sMiddName = "" Then
                        MessageBox.Show("No Spouse Middlename entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn03.Focus()
                        Return False
                    ElseIf Not IsDate(.spouse_info.personal_info.dBirthDte) Then
                        MessageBox.Show("Invalid Spouse Birth date entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn05.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sBirthPlc = "" Then
                        MessageBox.Show("No Spouse Birth Place entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn06.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.sCitizenx = "" Then
                        MessageBox.Show("No Spouse Citizenship entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        txtSpoIn07.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.cCvilStat = "" Then
                        MessageBox.Show("No Spouse Civil Status entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        cmb33.Focus()
                        Return False
                    ElseIf .spouse_info.personal_info.cGenderCd = "" Then
                        MessageBox.Show("No Spouse Gender entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 8
                        cmb34.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.cOwnershp = "" Then
                        MessageBox.Show("No Spouse Home ownership entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 9
                        cmb80.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.present_address.sAddress1 = "" And .spouse_info.residence_info.present_address.sAddress2 = "" Then
                        MessageBox.Show("No Spouse Present Phase #/Lot #/Sitio or Street Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe02.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.present_address.sTownIDxx = "" Then
                        MessageBox.Show("No Spouse present Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe04.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.present_address.sBrgyIDxx = "" Then
                        MessageBox.Show("No Spouse present barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe05.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.permanent_address.sAddress1 = "" And .spouse_info.residence_info.permanent_address.sAddress2 = "" Then
                        MessageBox.Show("No Spouse Permanent Phase #/Lot #/Sitio or Street Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe11.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.permanent_address.sTownIDxx = "" Then
                        MessageBox.Show("No Spouse permanent Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 9
                        txtSpoRe13.Focus()
                        Return False
                    ElseIf .spouse_info.residence_info.permanent_address.sBrgyIDxx = "" Then
                        MessageBox.Show("No Spouse permanent barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            End If
            If Not IsNothing(.comaker_info) Then
                If .comaker_info.sLastName <> "" Or .comaker_info.sLastName <> "" Then
                    If .comaker_info.sLastName = "" Then
                        MessageBox.Show("No Comaker Last Name detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 11
                        txtCoMak00.Focus()
                        Return False
                    ElseIf .comaker_info.sLastName = "" Then
                        MessageBox.Show("No Comaker Last Name detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 11
                        txtCoMak01.Focus()
                        Return False
                    ElseIf .comaker_info.sMiddName = "" Then
                        MessageBox.Show("No Comaker Middle Name detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 11
                        txtCoMak02.Focus()
                        Return False
                    ElseIf Not IsDate(.comaker_info.dBirthDte) Then
                        MessageBox.Show("No Comaker Birthdate detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 11
                        txtCoMak05.Focus()
                        Return False
                    ElseIf .comaker_info.sBirthPlc = "" Then
                        MessageBox.Show("No Comaker Birth Place detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 11
                        txtCoMak06.Focus()
                        Return False
                    ElseIf .comaker_info.residence_info.cOwnershp = "" Then
                        MessageBox.Show("No Comaker Home Ownership entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 12
                        cmb54.Focus()
                        Return False
                    ElseIf .comaker_info.residence_info.present_address.sAddress1 = "" And .residence_info.present_address.sAddress2 = "" Then
                        MessageBox.Show("No present Phase #/Lot #/Sitio or Street name entry detected at Comaker", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 12
                        txtCoAdd03.Focus()
                        Return False
                    ElseIf .comaker_info.residence_info.present_address.sTownIDxx = "" Then
                        MessageBox.Show("No present Town/City entry detected at Comaker", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 12
                        txtCoAdd04.Focus()
                        Return False
                    ElseIf .comaker_info.residence_info.present_address.sBrgyIDxx = "" Then
                        MessageBox.Show("No present barangay entry detected at Comaker", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        tabControl00.SelectedIndex = 12
                        txtCoAdd05.Focus()
                        Return False
                    End If
                End If
            End If

        End With
        Return True
    End Function

    Private Sub cmb_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim loChk As ComboBox
        loChk = CType(sender, System.Windows.Forms.ComboBox)
        If e.KeyCode = Keys.Back Then

            Dim lnIndex As Integer
            With p_oTrans.Category
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
                        .disbursement_info.properties.cWith3Whl = ""
                        loChk.SelectedIndex = -1
                    Case 20
                        .disbursement_info.properties.cWith2Whl = ""
                        loChk.SelectedIndex = -1
                    Case 21
                        .disbursement_info.properties.cWithRefx = ""
                        loChk.SelectedIndex = -1
                    Case 22
                        .disbursement_info.properties.cWithTVxx = ""
                        loChk.SelectedIndex = -1
                    Case 23
                        .disbursement_info.properties.cWithACxx = ""
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
                        .disbursement_info.properties.cWith4Whl = ""
                        loChk.SelectedIndex = -1
                    Case 63
                        .disbursement_info.dependent_info.children(pnRow).sRelatnCD = ""
                        loChk.SelectedIndex = -1
                    Case 64
                        .disbursement_info.dependent_info.children(pnRow).cIsPupilx = ""
                        loChk.SelectedIndex = -1
                    Case 65
                        .disbursement_info.dependent_info.children(pnRow).cIsPrivte = ""
                        loChk.SelectedIndex = -1
                    Case 66
                        .disbursement_info.dependent_info.children(pnRow).sEducLevl = ""
                        loChk.SelectedIndex = -1
                    Case 67
                        .disbursement_info.dependent_info.children(pnRow).cIsSchlrx = ""
                        loChk.SelectedIndex = -1
                    Case 68
                        .disbursement_info.dependent_info.children(pnRow).cHasWorkx = ""
                        loChk.SelectedIndex = -1
                    Case 69
                        .disbursement_info.dependent_info.children(pnRow).cWorkType = ""
                        loChk.SelectedIndex = -1
                    Case 50
                        .disbursement_info.dependent_info.children(pnRow).cHouseHld = ""
                        loChk.SelectedIndex = -1
                    Case 51
                        .disbursement_info.dependent_info.children(pnRow).cDependnt = ""
                        loChk.SelectedIndex = -1
                    Case 52
                        .disbursement_info.dependent_info.children(pnRow).cIsChildx = ""
                        loChk.SelectedIndex = -1
                    Case 53
                        .disbursement_info.dependent_info.children(pnRow).cIsMarrdx = ""
                        loChk.SelectedIndex = -1
                    Case 54
                        .comaker_info.residence_info.cOwnershp = ""
                        loChk.SelectedIndex = -1
                    Case 55
                        .comaker_info.residence_info.cOwnOther = ""
                        loChk.SelectedIndex = -1
                    Case 56
                        .comaker_info.residence_info.rent_others.cRntOther = ""
                        loChk.SelectedIndex = -1
                    Case 57
                        .comaker_info.residence_info.cHouseTyp = ""
                        loChk.SelectedIndex = -1
                    Case 58
                        .comaker_info.residence_info.cGaragexx = ""
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
                        If (TypeOf loTxt Is ComboBox) Then
                            DirectCast(loTxt, ComboBox).SelectedIndex = -1
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub clearSpouseDetails()
        With p_oTrans.Category
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
        End With
    End Sub


    Public Function isWithSpouse(ByVal groupbox As GroupBox) As Boolean
        Dim txt As Control
        For Each txt In groupbox.Controls
            If TypeOf txt Is TextBox Then
                If txt.Text <> "" Then
                    Dim ans As String
                    ans = MsgBox("Selected customer's civil status doesn't require spouse information!" & vbCrLf & _
                                 "By proceeding all info of spouse will be remove...", vbCritical + vbYesNo, "Confirm")
                    If ans = vbYes Then
                        ClearSpouseInfo(Me.grpBox17)
                        ClearSpouseInfo(Me.grpBox19)
                        ClearSpouseInfo(Me.grpBox21)
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

    Private Sub showCICriteria()
        With p_oValidate
            Select Case p_oValidate.isRecordExist()
                Case 0
                    Call showCITagging()
                Case 1
                    Call showCITaggingView()
            End Select
        End With
    End Sub
End Class
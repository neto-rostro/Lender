Option Explicit On
Imports ggcGOCAS
Imports ggcAppDriver
Imports ggcGOCAS.GOCASCI
Imports Newtonsoft.Json

Public Class frmMarketplaceHistory
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

    Private WithEvents p_oTrans As ggcGOCAS.GOCASMarketplace
    Dim psTransNox As String

    Public WriteOnly Property sTransNox
        Set(ByVal value)
            psTransNox = value
        End Set
    End Property

    Private Sub frmMarketplaceHistory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMarketplaceHistory")
        If pnLoadx = 1 Then
            Call p_oTrans.NewTransaction()
            ClearFields(Me.Panel1)
            ClearFields(Me.Panel2)

            'initButton(0)
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
            p_oTrans = New ggcGOCAS.GOCASMarketplace(p_oAppDriver, 30)

            'Call grpCancelHandler(Me, GetType(TextBox), "txtIntro", "Validating", AddressOf txtIntro_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtCoMak", "Validating", AddressOf txtCoMak_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtPerso", "Validating", AddressOf txtPerso_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtResid", "Validating", AddressOf txtResid_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtEmplo", "Validating", AddressOf txtEmplo_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtDisbu", "Validating", AddressOf txtDisbu_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtSpoIn", "Validating", AddressOf txtSpoIn_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtSpoEm", "Validating", AddressOf txtSpoEm_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtOther", "Validating", AddressOf txtOther_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtSpoRe", "Validating", AddressOf txtSpoRe_Validating)
            'Call grpCancelHandler(Me, GetType(TextBox), "txtCoAdd", "Validating", AddressOf txtComakRes_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtIntro", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDisbu", "GotFocus", AddressOf txtField_GotFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtIntro", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtResid", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDisbu", "KeyDown", AddressOf txtField_KeyDown)

            'Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtIntro", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtCoMak", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtResid", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtDisbu", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtSpoIn", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtSpoEm", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtOther", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtSpoRe", "LostFocus", AddressOf txtField_LostFocus)
            'Call grpEventHandler(Me, GetType(TextBox), "txtCoAdd", "LostFocus", AddressOf txtField_LostFocus)

            'Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtIntro", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtCoMak", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtResid", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtDisbu", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtSpoIn", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtSpoEm", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtOther", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtSpoRe", "KeyDown", AddressOf ArrowKeys_Keydown)
            'Call grpKeyHandler(Me, GetType(TextBox), "txtCoAdd", "KeyDown", AddressOf ArrowKeys_Keydown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            'Call grpKeyHandler(Me, GetType(ComboBox), "cmb", "KeyDown", AddressOf cmb_KeyDown)
            'Call grpEventHandler(Me, GetType(ComboBox), "cmb", "SelectedIndexChanged", AddressOf combobox_SelectedIndexChanged)

            pnLoadx = 1
        End If
    End Sub

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
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
                        ' loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.other_info.personal_reference(pdRow).sRefrTown)
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
                        '    loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.applicant_info.sBirthPlc)
                    Case 7
                        '     loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.applicant_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtResid" Then
                'Select Case loIndex
                '    Case 5
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.residence_info.present_address.sTownIDxx)
                '    Case 6
                '        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.residence_info.present_address.sBrgyIDxx, p_oTrans.Category.residence_info.present_address.sTownIDxx)
                '    Case 16
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.residence_info.permanent_address.sTownIDxx)
                '    Case 17
                '        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.residence_info.permanent_address.sBrgyIDxx, p_oTrans.Category.residence_info.permanent_address.sTownIDxx)
                'End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtCoMak" Then
                Select Case loIndex
                    Case 6
                        'loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.comaker_info.sBirthPlc)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoIn" Then
                Select Case loIndex
                    Case 6
                        'loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_info.personal_info.sBirthPlc)
                    Case 7
                        'loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.spouse_info.personal_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoRe" Then
                'Select Case loIndex
                '    Case 4
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.present_address.sTownIDxx)
                '    Case 5
                '        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.present_address.sBrgyIDxx, p_oTrans.Category.spouse_info.residence_info.present_address.sTownIDxx)
                '    Case 13
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.permanent_address.sTownIDxx)
                '    Case 14
                '        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.spouse_info.residence_info.permanent_address.sBrgyIDxx, p_oTrans.Category.spouse_info.residence_info.permanent_address.sTownIDxx)
                'End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpoEm" Then
                'Select Case loIndex
                '    Case 3
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_means.employed.sWrkTownx)
                '    Case 4
                '        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Category.spouse_means.employed.sPosition)
                '    Case 13
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.spouse_means.self_employed.sBusTownx)
                '    Case 20
                '        loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.spouse_means.employed.sOFWNatnx)
                'End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtEmplo" Then
                Select Case loIndex
                    Case 0
                        'loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sOFWNatnx)
                    Case 5
                        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sPosition)
                    Case 4
                        'loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sWrkTownx)
                    Case 14
                        'loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.means_info.self_employed.sBusTownx)
                    Case 20
                        ' loTxt.Text = p_oTrans.getCountry(loTxt.Text, True, False, p_oTrans.Category.means_info.financed.sNatnCode)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtDisbu" Then
                Select Case loIndex
                    Case 16
                        'If p_oTrans.Category.disbursement_info.dependent_info.children.Count = 0 Then
                        '    p_oTrans.Category.disbursement_info.dependent_info.children.Add(New GOCASConstMarketplace.children_param)
                        'End If
                        ' loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.disbursement_info.dependent_info.children(pnRow).sSchlTown)

                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtCoAdd" Then
                'Select Case loIndex
                '    Case 4
                '        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.comaker_info.residence_info.present_address.sTownIDxx)
                '    Case 5
                '        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.comaker_info.residence_info.present_address.sBrgyIDxx, p_oTrans.Category.comaker_info.residence_info.present_address.sTownIDxx)
                'End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        Dim lsName As String
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))
        With p_oTrans
            Select Case lnIndex

                Case 3 'Browse
                    If p_oTrans.SearchHistTransaction("%", False, True) = True Then
                        ClearFields(Me.Panel1)
                        ClearFields(Me.Panel2)
                        loadTransaction()
                    End If
            
                Case 0 ' Exit
                    Me.Dispose()
                
                Case 2 'void application
                    If IsDBNull(p_oTrans.Master("sTransNox")) Or txtField00.Text = "" Then
                        MsgBox("Unable to override this application please check entry...", vbCritical, "Error")
                    Else
                        If p_oTrans.DisapproveTransaction() Then
                            MsgBox("Application was DISAPPROVED successfully.", vbInformation, "Success")

                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            loadTransaction()
                        End If
                    End If

            End Select
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
                                Case 1
                                    loTxt.Text = .employed.sIndstWrk
                                    loTxt.Tag = .employed.sIndstWrk
                                Case 2
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                    loTxt.Tag = .employed.sPosition
                                Case 3
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                    loTxt.Tag = .employed.nSalaryxx
                                Case 4
                                    loTxt.Text = .self_employed.sIndstBus
                                    loTxt.Tag = .self_employed.sIndstBus
                                Case 5
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                                    loTxt.Tag = .self_employed.nBusIncom
                                Case 7
                                    loTxt.Text = Format(CDbl(.pensioner.nPensionx), xsDECIMAL)
                                    loTxt.Tag = .pensioner.nPensionx
                                Case 9
                                    loTxt.Text = Format(CDbl(.financed.nEstIncme), xsDECIMAL)
                                    loTxt.Tag = .financed.nEstIncme
                                Case 10
                                    loTxt.Text = .other_income.sOthrIncm
                                    loTxt.Tag = .other_income.sOthrIncm
                                Case 11
                                    loTxt.Text = Format(CDbl(.other_income.nOthrIncm), xsDECIMAL)
                                    loTxt.Tag = .other_income.nOthrIncm
                            End Select

                            If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb02)
                            If (.pensioner.cPenTypex <> "") Then setPensionType(.pensioner.cPenTypex, cmb03)
                            If (.financed.sReltnCde <> "") Then setFinanceType(.financed.sReltnCde, cmb04)
                        End With
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
                                    loTxt.Text = .bank_account.sBankName
                                    loTxt.Tag = .bank_account.sBankName
                            End Select
                            If (.bank_account.sAcctType <> "") Then setBankType(.bank_account.sAcctType, cmb05)
                        End With
                    End If
                End If
            End If
        Next
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
                                
                                Case 6
                                    loTxt.Text = .sMaidenNm
                                    loTxt.Tag = .sMaidenNm
                                Case 7
                                    loTxt.Text = .facebook.sFBAcctxx
                                    loTxt.Tag = .facebook.sFBAcctxx

                                Case 8
                                    loTxt.Text = .sLandMark
                                    loTxt.Tag = .sLandMark
                                Case 9
                                    If (.sHouseNox = "") Then
                                        loTxt.Text = ""
                                        loTxt.Tag = ""
                                    Else
                                        loTxt.Text = .sHouseNox
                                        loTxt.Tag = .sHouseNox
                                    End If
                                Case 10
                                    loTxt.Text = .sAddress1
                                    loTxt.Tag = .sAddress1
                                Case 11
                                    loTxt.Text = .sAddress2
                                    loTxt.Tag = .sAddress2
                                Case 12
                                    loTxt.Text = p_oTrans.getTownCity(.sTownIDxx, False, True, "")
                                    loTxt.Tag = .sTownIDxx
                                Case 13
                                    loTxt.Text = p_oTrans.getBarangay(.sBrgyIDxx, False, True, "")
                                    loTxt.Tag = .sBrgyIDxx
                            End Select
                            If (.cCvilStat <> "") Then setCivilStat(.cCvilStat, cmb00)
                            If (.cGenderCd <> "") Then setGender(.cGenderCd, cmb01)
                        End With
                    End If
                End If
            End If
        Next
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
    Private Sub loadTransaction()

        loadIntroQuestion(Me.tabPages00)
        loadMainInfo(Me.Panel1)
        loadAppliInfo(Me.tabPages01)
        loadAppEmplymnt(Me.tabPages02)
        loadDisburesement(Me.tabPages03)
        setTranStat(IFNull(p_oTrans.Master("cTranStat"), "-1"), lblStatus)
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
                                    loTxt.Text = p_oTrans.getBranch(.sBranchCd, False, True, "")
                                    loTxt.Tag = .sBranchCd
                                Case 2
                                    Select Case .cUnitAppl
                                        Case 0
                                            loTxt.Text = "Motorcycle"
                                        Case 1
                                            loTxt.Text = "Sidecar"
                                        Case 2
                                            loTxt.Text = "Others"
                                        Case 3
                                            loTxt.Text = "Mobile Phone"
                                        Case 4
                                            loTxt.Text = "Cars"
                                        Case 5
                                            loTxt.Text = "Services"
                                    End Select
                                Case 3
                                    loTxt.Text = Format(CDbl(.nDownPaym), xsDECIMAL)
                                    loTxt.Tag = .nDownPaym
                                Case 4
                                    If .dAppliedx = "" Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = Format(CDate(.dAppliedx), xsDATE_MEDIUM)
                                    End If
                                    loTxt.Tag = .dAppliedx
                                Case 5
                                    loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")
                                    loTxt.Tag = .sModelIDx
                                Case 6
                                    loTxt.Text = Format(CDbl(.nUnitPrce), xsDECIMAL)
                                    loTxt.Tag = .nUnitPrce
                                Case 7
                                    loTxt.Text = Format(CDbl(.nMonAmort), xsDECIMAL)
                                    loTxt.Tag = .nMonAmort
                                Case 8
                                    If Not IsNumeric(.nAcctTerm) Then .nAcctTerm = 0
                                    loTxt.Text = CInt(.nAcctTerm)
                                Case 9
                                    loTxt.Text = Format(CDbl(.nMonAmort), xsDECIMAL)
                                    loTxt.Tag = .nMonAmort
                            End Select
                        End With
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
                                Case 5
                                    Select Case .Category.cUnitAppl
                                        Case 0
                                            loTxt.Text = "Motorcycle"
                                        Case 1
                                            loTxt.Text = "Sidecar"
                                        Case 2
                                            loTxt.Text = "Others"
                                        Case 3
                                            loTxt.Text = "Mobile Phone"
                                        Case 4
                                            loTxt.Text = "Cars"
                                        Case 5
                                            loTxt.Text = "Services"
                                    End Select
                                Case 6
                                    loTxt.Text = p_oTrans.getModel(.Category.sModelIDx, False, True, "")
                                Case 7
                                    If Not IsNumeric(.Category.nAcctTerm) Then .Category.nAcctTerm = 0
                                    loTxt.Text = CInt(.Category.nAcctTerm)
                                Case 8
                                    If Not IsNumeric(.Detail.nDownPaym) Then .Category.nDownPaym = 0
                                    loTxt.Text = FormatNumber(.Category.nDownPaym, 2)
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
                                    loTxt.Text = .Category.applicant_info.sHouseNox + " " + _
                                        p_oTrans.getTownCity(.Category.applicant_info.sTownIDxx, False, True, "") + " " + .Category.applicant_info.sAddress1

                            End Select
                        End With
                    End If
                End If
            End If
        Next

        p_oValidate = New GOCASCI(p_oAppDriver)
        p_oValidate.TransNo = txtField00.Text
    End Sub
End Class
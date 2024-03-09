Option Explicit On
Imports ggcGOCAS
Imports ggcAppDriver
Imports rmjGOCAS
Imports ggcGOCAS.GOCASCI
Imports Newtonsoft.Json

Public Class frmMCCreditAppOverride
    Public poTrans As GOCASCI
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private pnRow As Integer
    Dim selRow As Integer
    Dim selCol As Integer
    Dim loFrm As frmMCCreditAppOverride
    Dim psValue As String
    Private lsAmortization As Decimal
    Private WithEvents p_oTrans As ggcGOCAS.GOCASApplication

    Private Sub frmMCCreditAppOverride_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMCCreditAppOverride_Activated")
        If pnLoadx = 1 Then
            initGrid(dgv01)
            initReference(dgv00)
            Call p_oTrans.NewTransaction()
            ClearFields(Me.Panel1)
            ClearFields(Me.Panel2)
            initButton(0)
            pnLoadx = 2
        End If
    End Sub


    Private Sub frmMCCreditAppReview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Debug.Print("frmMCCreditAppOverride_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcGOCAS.GOCASApplication(p_oAppDriver, 13)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "GotFocus", AddressOf txtField_GotFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf txtField_KeyDown)
            Call grpCancelHandler(Me, GetType(TextBox), "txtPerso", "Validating", AddressOf txtPerso_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "LostFocus", AddressOf txtField_LostFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpous", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtComak", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpoem", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtIncom", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtFinan", "KeyDown", AddressOf ArrowKeys_Keydown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub loadIncome(ByVal loControl As Control)
        If IsNothing(p_oTrans.Detail.disbursement_info) Then Exit Sub
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadIncome(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtIncom" Then
                        With IIf(psValue = "0", p_oTrans.Detail, p_oTrans.Category)
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = Format(CDbl(.means_info.self_employed.nBusIncom) + CDbl(.means_info.employed.nSalaryxx), xsDECIMAL)
                                Case 1
                                    loTxt.Text = Format(CDbl(IIf(.means_info.other_income.nOthrIncm = "", 0, .means_info.other_income.nOthrIncm)), xsDECIMAL)
                                Case 2
                                    loTxt.Text = Format(CDbl(.means_info.self_employed.nBusIncom) + CDbl(.means_info.employed.nSalaryxx) + CDbl(IIf(.means_info.other_income.nOthrIncm = "", 0, .means_info.other_income.nOthrIncm)), xsDECIMAL)
                                Case 3
                                    loTxt.Text = Format(CDbl(.disbursement_info.monthly_expenses.nElctrcBl), xsDECIMAL)
                                Case 4
                                    loTxt.Text = Format(CDbl(.disbursement_info.monthly_expenses.nWaterBil), xsDECIMAL)
                                Case 5
                                    loTxt.Text = Format(CDbl(.disbursement_info.monthly_expenses.nFoodAllw), xsDECIMAL)
                                Case 6
                                    loTxt.Text = Format(CDbl(.disbursement_info.monthly_expenses.nLoanAmtx), xsDECIMAL)
                                Case 7
                                    loTxt.Text = Format(CDbl(0), xsDECIMAL)
                                Case 8
                                    loTxt.Text = Format(CDbl(.disbursement_info.monthly_expenses.nElctrcBl) + CDbl(.disbursement_info.monthly_expenses.nWaterBil + CDbl(.disbursement_info.monthly_expenses.nFoodAllw) + CDbl(.disbursement_info.monthly_expenses.nLoanAmtx)), xsDECIMAL)
                                Case 9
                                    loTxt.Text = Format((CDbl(.means_info.self_employed.nBusIncom) + CDbl(.means_info.employed.nSalaryxx) + CDbl(IIf(.means_info.other_income.nOthrIncm = "", 0, .means_info.other_income.nOthrIncm))) - (CDbl(.disbursement_info.monthly_expenses.nElctrcBl) + CDbl(.disbursement_info.monthly_expenses.nWaterBil + CDbl(.disbursement_info.monthly_expenses.nFoodAllw) + CDbl(.disbursement_info.monthly_expenses.nLoanAmtx))), xsDECIMAL)
                            End Select
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadMainInfo(ByVal poControl As Control)

        Dim loTxt As Control
        For Each loTxt In poControl.Controls
            If loTxt.HasChildren Then
                Call loadMainInfo(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then

                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    psValue = IIf(IFNull(p_oTrans.Master("sCatInfox"), "") = "", "0", "1")
                    With IIf(psValue = 1, p_oTrans.Category, p_oTrans.Detail)
                        If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                            Select Case loIndex

                                Case 0
                                    loTxt.Text = IFNull(p_oTrans.Master("sTransNox"), "")
                                Case 1
                                    loTxt.Text = IIf(.applicant_info.sNickName = "", "N/A", .applicant_info.sNickName)
                                Case 2
                                    loTxt.Text = IFNull(p_oTrans.Master("sClientNm"), "")
                                Case 3
                                    If Not IsDate(.applicant_info.dBirthDte) Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.applicant_info.dBirthDte), xsDATE_MEDIUM)
                                    End If
                                Case 4
                                    If Not IsDate(.applicant_info.dBirthDte) Then
                                        loTxt.Text = Format(DateDiff("M", p_oAppDriver.getSysDate, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    Else
                                        loTxt.Text = Format(DateDiff("M", .applicant_info.dBirthDte, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    End If
                                Case 5
                                    loTxt.Text = IIf(.applicant_info.sBirthPlc = "", "N/A", p_oTrans.getTownCity(.applicant_info.sBirthPlc, False, True, ""))
                                Case 6
                                    loTxt.Text = IIf(.applicant_info.mobile_number(0).sMobileNo = "", "N/A", .applicant_info.mobile_number(0).sMobileNo)
                                Case 8
                                    loTxt.Text = .residence_info.present_address.sLandMark + " " + .residence_info.present_address.sHouseNox + " " + .residence_info.present_address.sAddress1 + " " + .residence_info.present_address.sAddress2 + _
                                        p_oTrans.getTownCity(.residence_info.present_address.sTownIDxx, False, True, "") + " " + p_oTrans.getBarangay(.residence_info.present_address.sBrgyIDxx, False, True, "")
                            End Select
                            setCivilStat(.applicant_info.cCvilStat, cmb03)
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        Dim loindex As Integer
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
        loindex = Val(Mid(loTxt.Name, 9))
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))
        With p_oTrans
            Select Case lnIndex
                Case 0 ' Exit
                    Me.Dispose()
                Case 1 'Override
                    If p_oTrans.Master("cTranStat") = "4" Then
                        MsgBox("Transaction already voided! Unable to override voided application...", vbCritical, "Error")
                        Exit Sub
                    End If

                    If IsDBNull(p_oTrans.Master("sTransNox")) Or txtField00.Text = "" Or IsDBNull(p_oTrans.Master("sGOCASNox")) Then
                        MsgBox("Unable to override this application please check entry...", vbCritical, "Error")
                        Exit Sub
                    End If

                    If Not p_oTrans.isUserHighRank Then
                        MsgBox("User is not allowed to OVERRIDE an application.", vbCritical, "Error")
                        Exit Sub
                    End If
                    initButton(1)
                Case 2 'Disapprove Application
                    'mac 2021.06.16
                    If IsDBNull(p_oTrans.Master("sTransNox")) Or txtField00.Text = "" Or IsDBNull(p_oTrans.Master("sGOCASNox")) Then
                        MsgBox("Unable to override this application please check entry...", vbCritical, "Error")
                    Else
                        If p_oTrans.DisapproveTransaction() Then
                            MsgBox("Application was DISAPPROVED successfully.", vbInformation, "Success")

                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            loadTransaction()
                        End If
                    End If
                Case 4 'save
                    If IsDBNull(p_oTrans.Master("sTransNox")) Or txtField00.Text = "" Or IsDBNull(p_oTrans.Master("sGOCASNox")) Then
                        MsgBox("Unable to save this application please check entry...", vbCritical, "Error")
                        Exit Sub
                    End If

                    If IsNumeric(txtPerso04.Text) = True Then
                        If p_oTrans.overrideResult(CDbl(txtPerso04.Text), lsAmortization) Then
                            MsgBox("Downpayment successfully overide...", vbInformation, "Information")
                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            p_oTrans.OpenTransaction(p_oTrans.Master("sTransNox"))
                            Call loadTransaction()
                            initButton(0)
                        End If
                    Else
                        ClearFields(Me.Panel1)
                        ClearFields(Me.Panel2)
                        p_oTrans.OpenTransaction(p_oTrans.Master("sTransNox"))
                        Call loadTransaction()
                        initButton(0)
                    End If
                    
                Case 5 'cancel transaction
                    If MsgBox("Do you want undo changes for this transaction?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        Call p_oTrans.NewTransaction()
                        ClearFields(Me.Panel1)
                        ClearFields(Me.Panel2)
                        initButton(0)
                    End If

                Case 3 ' browse
                    If p_oTrans.SearchTransaction("%", False) = True Then
                        ClearFields(Me.Panel1)
                        ClearFields(Me.Panel2)
                        loadTransaction()
                    End If
                Case 14
                    showResult()
                Case 15
                    If MsgBox("Do you want to void this credit application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        If p_oTrans.CancelTransaction Then
                            MsgBox("Credit Application successfully voided!!", vbInformation, "Information")
                            Call p_oTrans.NewTransaction()
                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            initButton(0)
                        End If
                    End If
                Case 25
                    If txtField00.Text = "" Then Exit Sub

                    poTrans = New GOCASCI(p_oAppDriver)
                    With poTrans
                        poTrans.TransNo = txtField00.Text
                        If poTrans.isRecordExist() Then
                            Call showCITaggingView()
                        Else
                            Call showCITagging()
                        End If
                    End With
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

    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub loadPerso(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadPerso(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtPerso" Then
                        With IIf(psValue = "0", p_oTrans.Detail, p_oTrans.Category)
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = Format(CDate(.dAppliedx), xsDATE_MEDIUM)
                                Case 1
                                    loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")
                                Case 4
                                    Dim instance As New GOCASCodeGen
                                    If (IFNull(p_oTrans.Master("sGOCASNoF"), "") = "") Then
                                        instance.Decode(p_oTrans.Master("sGOCASNox"))
                                        loTxt.Text = IIf(instance.DownPayment = 200, "DEFAULT", p_oTrans.Master("nDownPaym") & "%")
                                    Else
                                        instance.Decode(p_oTrans.Master("sGOCASNoF"))
                                        loTxt.Text = IIf(instance.DownPayment = 200, "DEFAULT", p_oTrans.Master("nDownPayF") & "%")
                                    End If
                                Case 5
                                    loTxt.Text = .sUnitAppl
                                Case 6
                                    loTxt.Text = CInt(.nAcctTerm)
                                Case 7
                                    loTxt.Text = IIf(lsAmortization <> 0, Format(lsAmortization, xsDECIMAL), Format(CDbl(.nMonAmort), xsDECIMAL))
                                Case 9
                                    loTxt.Text = IFNull(p_oTrans.Master("sQMatchNo"), "N/A")
                            End Select
                            setCustomerType(.cApplType, cmb04)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadSpouse(ByVal loControl As Control)
        If IsNothing(p_oTrans.Detail.spouse_info) Then Exit Sub
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouse(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Then
                        With IIf(psValue = "0", p_oTrans.Detail, p_oTrans.Category)
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .spouse_info.personal_info.sLastName + " ," + .spouse_info.personal_info.sFrstName
                                Case 1
                                    loTxt.Text = .spouse_info.residence_info.present_address.sLandMark + " " + .spouse_info.residence_info.present_address.sHouseNox + " " + .spouse_info.residence_info.present_address.sAddress1 + " " + .spouse_info.residence_info.present_address.sAddress2 + _
                                        p_oTrans.getTownCity(.spouse_info.residence_info.present_address.sTownIDxx, False, True, "") + " " + p_oTrans.getBarangay(.spouse_info.residence_info.present_address.sBrgyIDxx, False, True, "")
                                Case 2
                                    For lnCtr As Integer = 0 To .spouse_info.personal_info.mobile_number.Count - 1
                                        Select Case lnCtr
                                            Case 0
                                                loTxt.Text = .spouse_info.personal_info.mobile_number(lnCtr).sMobileNo
                                        End Select
                                    Next
                                Case 3
                                    loTxt.Text = .spouse_info.personal_info.dBirthDte
                                Case 4
                                    loTxt.Text = p_oTrans.getTownCity(.spouse_info.residence_info.present_address.sTownIDxx, False, True, "")
                                Case 5
                                    For lnCtr As Integer = 0 To .spouse_info.personal_info.landline.Count - 1
                                        Select Case lnCtr
                                            Case 0
                                                loTxt.Text = .spouse_info.personal_info.landline(lnCtr).sPhoneNox
                                        End Select
                                    Next
                                Case 6
                                    If Not IsDate(.spouse_info.personal_info.dBirthDte) Then
                                        loTxt.Text = Format(DateDiff("M", p_oAppDriver.getSysDate, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    Else
                                        loTxt.Text = Format(DateDiff("M", .spouse_info.personal_info.dBirthDte, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    End If
                            End Select

                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadComak(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadComak(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtComak" Then
                        With (IIf(psValue = "0", p_oTrans.Detail.comaker_info, p_oTrans.Category.comaker_info))
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sLastName + ", " + .sFrstName
                                Case 1
                                    loTxt.Text = p_oTrans.getTownCity(.sBirthPlc, False, True, "")
                                Case 2
                                    For lnCtr As Integer = 0 To .mobile_number.Count - 1
                                        Select Case lnCtr
                                            Case 0
                                                loTxt.Text = .mobile_number(lnCtr).sMobileNo
                                        End Select
                                    Next
                            End Select
                            setRelation(.sReltnCde, cmb10)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadApplicantEmployment(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadApplicantEmployment(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Then
                        With (IIf(psValue = "0", p_oTrans.Detail.means_info, p_oTrans.Category.means_info))
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .employed.sEmployer
                                Case 1
                                    loTxt.Text = .employed.sWrkAddrx
                                Case 2
                                    loTxt.Text = p_oTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                Case 3
                                    loTxt.Text = .employed.sWrkTelno
                                Case 4
                                    loTxt.Text = .employed.nLenServc
                                Case 5
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                Case 6
                                    loTxt.Text = .employed.sFunction
                                Case 7
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                Case 8
                                    If Not IsNothing(.other_income) Then
                                        loTxt.Text = IIf(.other_income.nOthrIncm <> "", Format(.other_income.nOthrIncm, xsDECIMAL), 0)
                                    End If
                                Case 10
                                    loTxt.Text = .self_employed.sIndstBus
                                Case 11
                                    loTxt.Text = .self_employed.sBusAddrx
                                Case 12
                                    loTxt.Text = p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                Case 14
                                    loTxt.Text = .self_employed.nBusLenxx
                                Case 15
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                            End Select
                            setEmploymn(.employed.cEmpStatx, cmb05)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadSpouseEmployment(ByVal loControl As Control)
        If IsNothing(p_oTrans.Detail.spouse_means) Then Exit Sub
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseEmployment(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoEm" Then
                        With IIf(psValue = "0", p_oTrans.Detail.spouse_means, p_oTrans.Category.spouse_means)
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .employed.sEmployer
                                Case 1
                                    loTxt.Text = .employed.sWrkAddrx
                                Case 2
                                    loTxt.Text = p_oTrans.getTownCity(.employed.sWrkTownx, False, True, "")
                                Case 3
                                    loTxt.Text = .employed.sWrkTelno
                                Case 4
                                    loTxt.Text = .employed.nLenServc
                                Case 5
                                    loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                Case 6
                                    loTxt.Text = .employed.sFunction
                                Case 7
                                    loTxt.Text = Format(CDbl(.employed.nSalaryxx), xsDECIMAL)
                                Case 10
                                    loTxt.Text = .self_employed.sIndstBus
                                Case 11
                                    loTxt.Text = .self_employed.sBusAddrx
                                Case 12
                                    loTxt.Text = p_oTrans.getTownCity(.self_employed.sBusTownx, False, True, "")
                                Case 14
                                    loTxt.Text = .self_employed.nBusLenxx
                                Case 15
                                    loTxt.Text = Format(CDbl(.self_employed.nBusIncom), xsDECIMAL)
                            End Select
                            setEmploymn(.employed.cEmpStatx, cmb06)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadFinancer(ByVal loControl As Control)
        If IsNothing(p_oTrans.Detail.means_info.financed) Then Exit Sub
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadFinancer(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtFinan" Then
                        With IIf(psValue = "0", p_oTrans.Detail.means_info.financed, p_oTrans.Category.means_info.financed)
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sFinancer
                                Case 2
                                    loTxt.Text = .sMobileNo
                                Case 3
                                    loTxt.Text = p_oTrans.getCountry(.sNatnCode, False, True, "")
                                Case 4
                                    loTxt.Text = .sFBAcctxx
                                Case 5
                                    loTxt.Text = .sEmailAdd
                                Case 6
                                    loTxt.Text = Format(CDbl(.nEstIncme), xsDECIMAL)
                            End Select
                            setFinance(.sReltnCde, cmb91)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadTransaction()
        Call loadMainInfo(Me.Panel2)
        Call loadMainInfo(Me.grpBox00)
        Call loadPerso(Me.tabPage00)
        Call loadSpouse(Me.tabPage01)
        Call loadComak(Me.tabPage01)
        Call showReference_Info()
        Call loadApplicantEmployment(Me.tabPage03)
        Call loadSpouseEmployment(Me.tabPage04)
        Call loadIncome(Me.tabPage05)
        Call loadFinancer(Me.tabPage06)
        Call showDependent_Info()
        Call showResult()
        setTranStat(IFNull(p_oTrans.Master("cTranStat"), "-1"), lblStatus)

    End Sub

    Private Sub showResult()
        Dim loFrm = New frmMCCreditAppResult
        If Not IsDBNull(p_oTrans.Master("nCrdtScrx")) And Not IsDBNull(p_oTrans.Master("sGOCASNox")) Then
            loFrm.clearFields()
            loFrm.sTransNox = IFNull(p_oTrans.Master("sTransNox"))
            loFrm.GoCasNoF = IFNull(p_oTrans.Master("sGOCASNoF"), "")
            loFrm.GoCasNo = IFNull(p_oTrans.Master("sGOCASNox"), "")
            loFrm.CreditScore = IFNull(p_oTrans.Master("nCrdtScrx"), "")
            loFrm.WithCI = IFNull(p_oTrans.Master("cWithCIxx"), "")
            loFrm.DownPaymentF = IIf(IFNull(p_oTrans.Master("nDownPayF"), 0) = 200, "DEFAULT", IIf(IFNull(p_oTrans.Master("nDownPayF"), 0) = 0, 0, p_oTrans.Master("nDownPayF")))
            loFrm.DownPayment = IIf(IFNull(p_oTrans.Master("nDownPaym"), 0) = 200, "DEFAULT", p_oTrans.Master("nDownPaym"))
            loFrm.ShowDialog()
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

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 50, 51

                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 50, True, False)) = True Then
                            ClearFields(Me.Panel1)
                            ClearFields(Me.Panel2)
                            loadTransaction()
                        End If
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub txtPerso_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        With p_oTrans
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))
            If Mid(loTxt.Name, 1, 8) = "txtPerso" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 4
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CDbl(loTxt.Text)
                        End If
                        loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                End Select
            End If
        End With
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtPerso" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtComak" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoem" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtIncom" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtFinan" Then
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
        lsAmortization = 0
        pnRow = -1
        setTranStat("-1", lblStatus)
        psValue = ""
    End Sub

    Private Sub showReference_Info()
        With IIf(psValue = "0", p_oTrans.Detail, p_oTrans.Category)
            If IsNothing(.other_info.personal_reference) Then Exit Sub
            dgv00.Rows.Clear()
            If .other_info.personal_reference.Count = 0 Then
                dgv00.Rows.Add()
                Exit Sub
            End If

            For lnCtr As Integer = 0 To .other_info.personal_reference.Count - 1
                dgv00.Rows.Add()
                dgv00.Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                dgv00.Rows(lnCtr).Cells(1).Value = .other_info.personal_reference(lnCtr).sRefrNmex
                dgv00.Rows(lnCtr).Cells(2).Value = .other_info.personal_reference(lnCtr).sRefrMPNx
                dgv00.Rows(lnCtr).Cells(3).Value = .other_info.personal_reference(lnCtr).sRefrAddx
                dgv00.Rows(lnCtr).Cells(4).Value = p_oTrans.getTownCity(.other_info.personal_reference(lnCtr).sRefrTown, False, True, "")
            Next
        End With
    End Sub

    Private Sub showDependent_Info()
        With IIf(psValue = "0", p_oTrans.Detail, p_oTrans.Category)
            If IsNothing(.disbursement_info.dependent_info) Then Exit Sub
            dgv01.Rows.Clear()
            If .disbursement_info.dependent_info.children.Count = 0 Then
                dgv01.Rows.Add()
                Exit Sub
            End If

            For lnCtr As Integer = 0 To .disbursement_info.dependent_info.children.Count - 1
                dgv01.Rows.Add()
                dgv01.Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                dgv01.Rows(lnCtr).Cells(1).Value = .disbursement_info.dependent_info.children(lnCtr).sFullName
                Select Case .disbursement_info.dependent_info.children(lnCtr).sRelatnCD
                    Case "0"
                        dgv01.Rows(lnCtr).Cells(2).Value = "Children"
                    Case "1"
                        dgv01.Rows(lnCtr).Cells(2).Value = "Parents"
                    Case "2"
                        dgv01.Rows(lnCtr).Cells(2).Value = "Siblings"
                    Case "3"
                        dgv01.Rows(lnCtr).Cells(2).Value = "Relatives"
                    Case Else
                        dgv01.Rows(lnCtr).Cells(2).Value = "Others"
                End Select
                dgv01.Rows(lnCtr).Cells(3).Value = .disbursement_info.dependent_info.children(lnCtr).nDepdAgex
            Next
        End With
    End Sub

    Private Sub initButton(ByVal fnValue As Integer)
        Dim lbShow As Integer
        lbShow = (fnValue = 1)

        cmdButton03.Visible = Not lbShow
        cmdButton01.Visible = Not lbShow
        cmdButton25.Visible = Not lbShow
        cmdButton00.Visible = Not lbShow
        cmdButton14.Visible = Not lbShow
        txtPerso04.ReadOnly = lbShow
        Me.Panel2.Enabled = Not lbShow

        txtPerso04.ReadOnly = Not lbShow
        cmdButton04.Visible = lbShow
        cmdButton05.Visible = lbShow
        Me.Panel1.Enabled = lbShow


        If lbShow Then
            txtPerso04.Focus()
        Else
            txtField50.Focus()
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
End Class
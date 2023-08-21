Imports ggcLRTransaction
Imports ggcAppDriver
Imports ggcGOCAS

Public Class frmCarApplicationHistory
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

    Private Sub frmCarApplicationHistory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmCarApplicationHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            p_oTrans = New ggcGOCAS.CARApplication(p_oAppDriver, 123450)


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

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
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

                Case 4 'Browse
                    If p_oTrans.SearchTransaction("%", False, False) = True Then
                        loadTransaction()
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
        Call ClearFields(Me.Panel1)
        Call ClearFields(Me.Panel2)

        Call loadIntroQuestion(Me.Panel1)
        Call loadMainInfo(Me.Panel1)
        Call loadOthers(Me.Panel1)
        Call loadComaker(Me.Panel1)
        Call loadSpouseInfo(Me.Panel1)
        Call loadSpouseRes(Me.Panel1)
        Call loadSpouseEmpl(Me.Panel1)
        Call loadAppliInfo(Me.Panel1)
        Call loadAppliRes(Me.Panel1)
        Call loadAppEmplymnt(Me.Panel1)
        Call loadDisburesement(Me.Panel1)
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
                                    If .dTargetDt = "" Then
                                        loTxt.Text = p_oAppDriver.SysDate
                                    Else
                                        loTxt.Text = .dTargetDt
                                    End If
                                    loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
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
                                Case 1
                                    loTxt.Text = p_oTrans.getBranch(p_oTrans.Master("sBranchCd"), False, True, "")
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
                                    If Not IsDate(.dBirthDte) Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                    End If
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
                                    If .dBirthDte = "" Then
                                        loTxt.Text = ""
                                    ElseIf .dBirthDte = "Invalid Date" Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                    End If
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
                                    If .dBirthDte = "" Then
                                        loTxt.Text = ""
                                    ElseIf Not IsDate(.dBirthDte) Then
                                        loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                                    Else
                                        loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                                    End If
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
                                    If (Not IsNothing(.rent_others)) Then
                                        loTxt.Text = .rent_others.nLenStayx
                                    End If
                                Case 7
                                    If Not IsNothing(.rent_others) Then
                                        loTxt.Text = .rent_others.nRentExps
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
                                    If Not IsNumeric(.other_income.nOthrIncm) Then
                                        loTxt.Text = CDbl(0)
                                    Else
                                        loTxt.Text = CInt(.pensioner.nRetrYear)
                                    End If
                                Case 26
                                    loTxt.Text = .other_income.sOthrIncm
                                Case 27
                                    If Not IsNumeric(.other_income.nOthrIncm) Then
                                        loTxt.Text = CDbl(0)
                                    Else
                                        loTxt.Text = Format(CDbl(.other_income.nOthrIncm), xsDECIMAL)
                                    End If
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
            txtDisbu12.Text = .children(fnRow).sFullName
            txtDisbu13.Text = .children(fnRow).nDepdAgex
            txtDisbu14.Text = .children(fnRow).sSchlName
            txtDisbu15.Text = .children(fnRow).sSchlAddr
            txtDisbu16.Text = p_oTrans.getTownCity(.children(fnRow).sSchlTown, False, True, "")
            txtDisbu17.Text = .children(fnRow).sCompanyx

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
            cmb64.SelectedIndex = -1
            cmb65.SelectedIndex = -1
            cmb67.SelectedIndex = -1
            cmb68.SelectedIndex = -1
            cmb69.SelectedIndex = -1
            cmb50.SelectedIndex = -1
            cmb51.SelectedIndex = -1
            cmb52.SelectedIndex = -1
            cmb53.SelectedIndex = -1
            cmb66.SelectedIndex = -1
            cmb63.SelectedIndex = -1
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
End Class
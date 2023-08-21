Option Explicit On
Imports ggcGOCAS
Imports ggcAppDriver
Imports ggcGOCAS.GOCASCI
Imports Newtonsoft.Json

Public Class frmMarketplace
    Public p_oValidate As GOCASCI
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private pnRow As Integer
    Private pdRow As Integer
    Private psButton As Integer
    Private psMobile As String
    Private psTransNox As String

    Private WithEvents p_oTrans As ggcGOCAS.GOCASMarketplace

    Public WriteOnly Property sTransNox
        Set(ByVal value)
            psTransNox = value
        End Set
    End Property

    Private Sub frmMarketplace_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMarketplace")
        If pnLoadx = 1 Then
            Call p_oTrans.NewTransaction()
            ClearFields(Me.Panel1)
            ClearFields(Me.Panel2)

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
    Private Sub frmMarketplace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmMCCreditAppCategorization_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcGOCAS.GOCASMarketplace(p_oAppDriver, 30)

            Call grpCancelHandler(Me, GetType(TextBox), "txtIntro", "Validating", AddressOf txtIntro_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtPerso", "Validating", AddressOf txtPerso_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtEmplo", "Validating", AddressOf txtEmplo_Validating)
            Call grpCancelHandler(Me, GetType(TextBox), "txtDisbu", "Validating", AddressOf txtDisbu_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtIntro", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDisbu", "GotFocus", AddressOf txtField_GotFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtIntro", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDisbu", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtIntro", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtPerso", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "LostFocus", AddressOf txtField_LostFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtDisbu", "LostFocus", AddressOf txtField_LostFocus)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtIntro", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtPerso", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtDisbu", "KeyDown", AddressOf ArrowKeys_Keydown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            'Call grpKeyHandler(Me, GetType(ComboBox), "cmb", "KeyDown", AddressOf cmb_KeyDown)
            Call grpEventHandler(Me, GetType(ComboBox), "cmb", "SelectedIndexChanged", AddressOf combobox_SelectedIndexChanged)

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

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
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
            ElseIf Mid(loTxt.Name, 1, 8) = "txtIntro" Then
                Select Case loIndex
                    Case 0
                        loTxt.Text = p_oTrans.getBranch(loTxt.Text, True, False, p_oTrans.Category.sBranchCd)
                    Case 5
                        loTxt.Text = p_oTrans.getModel(loTxt.Text, True, False, p_oTrans.Category.sModelIDx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtPerso" Then
                Select Case loIndex
                    Case 12
                        loTxt.Text = p_oTrans.getTownCity(loTxt.Text, True, False, p_oTrans.Category.applicant_info.sTownIDxx)
                    Case 13
                        loTxt.Text = p_oTrans.getBarangay(loTxt.Text, True, False, p_oTrans.Category.applicant_info.sBrgyIDxx, p_oTrans.Category.applicant_info.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtEmplo" Then
                Select Case loIndex

                    Case 2
                        loTxt.Text = p_oTrans.getOccupation(loTxt.Text, True, False, p_oTrans.Category.means_info.employed.sPosition)

                End Select

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
                        'displayMobile(p_oTrans.callApplicant)
                        psButton = "0"
                        initButton(1)
                    End If
               
                Case 1 ' approved
                    'computeCreditScore()
                    If txtField00.Text = "" Then Exit Sub
                    p_oValidate.TransNo = txtField00.Text

                    If (p_oTrans.Master("cTranstat") = 0) Then
                        If txtField00.Text <> "" Then
                            If p_oTrans.Approved(False) Then
                                MsgBox("Application successfully approved!!!", vbInformation, "Information")
                                ClearFields(Me.Panel1)
                                ClearFields(Me.Panel2)
                                p_oTrans.OpenTransaction(p_oTrans.Master("sTransNox"))
                                loadTransaction()
                            End If
                        End If
                    Else
                        MsgBox("Application is already approved!!!", vbInformation, "Information")
                    End If
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
                        'displayRederence(p_oTrans.callReference(lsName), lsName)

                        If psMobile <> "" Then
                            MsgBox("Not all reference numbers are called!", MsgBoxStyle.Exclamation, "Notice")
                            Exit Sub
                        End If
                    End If

                    Select Case p_oValidate.isRecordExist()
                        Case 0
                            If MsgBox("Do you want to set For-CI validation?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                                'showCITagging()
                            End If
                        Case 1
                            'showCITaggingView()
                    End Select
                Case 0 ' Exit
                    Me.Dispose()
                Case 2 'save
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
                Case 11 ' cancel
                    If MsgBox("Do you want to disregard all changes for this application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                        Call ClearFields(Me.Panel1)
                        Call ClearFields(Me.Panel2)
                        'clearDependent()
                        initButton(0)
                    End If
                
                Case 15 'void application
                    If txtField00.Text = "" Then Exit Sub
                    If (p_oTrans.Master("cTranstat") <> 0) Then
                        Dim lsSQL As String
                        lsSQL = "SELECT" & _
                                    " sPositnID" & _
                                " FROM Employee_Master001 a" & _
                                    ", xxxSysUser b" & _
                                " WHERE a.sEmployID = b.sEmployNo" & _
                                " AND b.sUserIDxx=" & strParm(p_oAppDriver.UserID)
                        Dim loDT As New DataTable
                        Try
                            loDT = p_oAppDriver.ExecuteQuery(lsSQL)
                            If loDT.Rows.Count = 1 Then
                                If loDT.Rows(0).Item("sPositnID") <> "126" And loDT.Rows(0).Item("sPositnID") <> "098" Then
                                    MsgBox("You are not allowed to void this application!!" + Environment.NewLine + "Please request assistance for supervisor.", vbCritical, "Warning-" & p_oAppDriver.UserID)
                                    Exit Sub
                                End If
                            End If
                        Catch ex As Exception
                            MsgBox(ex.Message)
                            Exit Sub
                        End Try

                        If MsgBox("Do you want to void this credit application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            If p_oTrans.CancelTransaction Then
                                MsgBox("Credit Application successfully voided!!", vbInformation, "Information")
                                Call p_oTrans.NewTransaction()
                                ClearFields(Me.Panel1)
                                ClearFields(Me.Panel2)
                                initButton(0)
                            End If
                        End If
                    Else
                        If MsgBox("Do you want to void this credit application?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                            If p_oTrans.CancelTransaction Then
                                MsgBox("Credit Application successfully voided!!", vbInformation, "Information")
                                Call p_oTrans.NewTransaction()
                                ClearFields(Me.Panel1)
                                ClearFields(Me.Panel2)
                                initButton(0)
                            End If
                        End If
                    End If

                    
            End Select
        End With
    End Sub
    Private Sub txtIntro_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        With p_oTrans.Category
            If Mid(loTxt.Name, 1, 8) = "txtIntro" And loTxt.ReadOnly = False Then
                Select Case loIndex
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nDownPaym = CDbl(loTxt.Text)
                    Case 4
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dTargetDt = CDate(loTxt.Text)
                    Case 5

                    Case 6
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nUnitPrce = CDbl(loTxt.Text)
                    Case 7

                    Case 8
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nAcctTerm = CDbl(loTxt.Text)
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .nMonAmort = CDbl(loTxt.Text)
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
                    Case 6
                        .sMaidenNm = loTxt.Text
                    Case 7
                        .facebook.sFBAcctxx = loTxt.Text
                    Case 8
                        .sLandMark = loTxt.Text
                    Case 9
                        .sHouseNox = loTxt.Text
                    Case 10
                        .sAddress1 = loTxt.Text
                    Case 11
                        .sAddress2 = loTxt.Text

                End Select
            End If
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
                        .employed.sPosition = loTxt.Text
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .employed.nSalaryxx = CDbl(loTxt.Text)
                    Case 4
                        .self_employed.sIndstBus = loTxt.Text
                    Case 5
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .self_employed.nBusIncom = CDbl(loTxt.Text)
                    Case 7
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .pensioner.nPensionx = CDbl(loTxt.Text)
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .financed.nEstIncme = CDbl(loTxt.Text)
                    Case 10
                        .other_income.sOthrIncm = loTxt.Text
                    Case 11
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = Format(0, xsDECIMAL)
                        Else
                            loTxt.Text = Format(CDbl(loTxt.Text), xsDECIMAL)
                        End If
                        .other_income.nOthrIncm = CDbl(loTxt.Text)
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
                        .bank_account.sBankName = loTxt.Text
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
                    .applicant_info.cCvilStat = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 2
                    .applicant_info.cGenderCd = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 3
                    .means_info.cIncmeSrc = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 4
                    .means_info.pensioner.cPenTypex = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 5
                    .means_info.financed.sReltnCde = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 6
                    .disbursement_info.bank_account.sAcctType = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)

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
                                Case 0
                                    loTxt.Text = .cIncmeSrc
                                    loTxt.Tag = .cIncmeSrc
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

                                Case 9
                                    loTxt.Text = Format(CDbl(.financed.nEstIncme), xsDECIMAL)
                                    loTxt.Tag = .financed.nEstIncme
                                Case 10
                                    loTxt.Text = .other_income.sOthrIncm
                                    loTxt.Tag = .other_income.sOthrIncm
                                Case 11
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
                                    Case 0
                                        'loTxt.Text = IIf(.employed.sOFWNatnx = "", "N/A", .employed.sOFWNatnx)
                                    Case 1
                                        loTxt.Text = IIf(.employed.sIndstWrk = "", "N/A", .employed.sIndstWrk)
                                    Case 2
                                        loTxt.Text = p_oTrans.getOccupation(.employed.sPosition, False, True, "")
                                    Case 3
                                        loTxt.Text = IIf(Format(CDbl(.employed.nSalaryxx), xsDECIMAL) = "", "", Format(CDbl(.employed.nSalaryxx), xsDECIMAL))
                                    Case 4
                                        loTxt.Text = IIf(.self_employed.sIndstBus = "", "N/A", .self_employed.sIndstBus)
                                    Case 5
                                        loTxt.Text = IIf(Format(CDbl(.self_employed.nBusIncom), xsDECIMAL) = "", "", Format(CDbl(.self_employed.nBusIncom), xsDECIMAL))
                                    Case 6
                                        loTxt.Text = IIf(.pensioner.cPenTypex = "", "N/A", .pensioner.cPenTypex)
                                    Case 7
                                        loTxt.Text = IIf(Format(CDbl(.pensioner.nPensionx), xsDECIMAL) = "", "", Format(CDbl(.pensioner.nPensionx), xsDECIMAL))
                                    Case 9
                                        loTxt.Text = IIf(Format(CDbl(.financed.nEstIncme), xsDECIMAL) = "", "", Format(CDbl(.financed.nEstIncme), xsDECIMAL))
                                    Case 10
                                        loTxt.Text = IIf(.other_income.sOthrIncm = "", "N/A", .other_income.sOthrIncm)
                                    Case 11
                                        loTxt.Text = IIf(Format(CDbl(.other_income.nOthrIncm), xsDECIMAL) = "", "", Format(CDbl(.other_income.nOthrIncm), xsDECIMAL))
                                        ' loTxt.Text = IIf(.other_income.nOthrIncm = "", "N/A", .other_income.nOthrIncm)
                                End Select
                                If (.cIncmeSrc <> "") Then setIncomeSource(.cIncmeSrc, cmb03, lb4Field00)
                                cmb03.Tag = .cIncmeSrc

                                If (.pensioner.cPenTypex <> "") Then setPensionType(.pensioner.cPenTypex, cmb04, lb4Field06)
                                cmb04.Tag = .pensioner.cPenTypex

                                If (.financed.sReltnCde <> "") Then setFinanceType(.financed.sReltnCde, cmb05, lb4Field08)
                                cmb05.Tag = .financed.sReltnCde

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
                                    loTxt.Text = .bank_account.sBankName
                                    loTxt.Tag = .bank_account.sBankName
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
                                        loTxt.Text = IIf(.bank_account.sBankName = "", "N/A", .bank_account.sBankName)
                                End Select
                                If (.bank_account.sAcctType <> "") Then setBankType(.bank_account.sAcctType, cmb06, lb5Field01)
                                cmb06.Tag = .bank_account.sAcctType
                            End With
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub initButton(ByVal fnValue As Integer)
        Dim lbShow As Integer
        lbShow = (fnValue = 1)

        Panel1.Enabled = Not lbShow
        cmdButton07.Visible = Not lbShow
        cmdButton08.Visible = Not lbShow
        cmdButton01.Visible = Not lbShow
        cmdButton00.Visible = Not lbShow
        cmdButton15.Visible = Not lbShow

        cmdButton02.Visible = lbShow
        cmdButton11.Visible = lbShow
        Panel1.Enabled = lbShow
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
                                    Case 6
                                        loTxt.Text = IIf(.sMaidenNm = "", "N/A", .sMaidenNm)
                                    Case 7
                                        loTxt.Text = IIf(.facebook.sFBAcctxx = "", "N/A", .facebook.sFBAcctxx)
                                    Case 8
                                        loTxt.Text = .sLandMark
                                        loTxt.Tag = .sLandMark
                                    Case 9
                                        loTxt.Text = .sHouseNox
                                        loTxt.Tag = .sHouseNox
                                    Case 10
                                        loTxt.Text = .sAddress1
                                        Debug.Print(.sAddress1 + " this address 1")
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
                                If (.cCvilStat <> "") Then setCivilStat(.cCvilStat, cmb01, lb2Field04)
                                cmb01.Tag = .cCvilStat
                                If (.cGenderCd <> "") Then setGender(.cGenderCd, cmb02, lb2Field05)
                                cmb02.Tag = .cGenderCd
                            End With
                        End If
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

        loadIntroQuestion(Me.tabPage00)
        loadMainInfo(Me.grpBox24)
        loadAppliInfo(Me.tabPage01)
        loadAppEmplymnt(Me.tabPage02)
        loadDisburesement(Me.tabPage03)
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
                                Case 3
                                    loTxt.Text = Format(CDbl(.nDownPaym), xsDECIMAL)
                                    loTxt.Tag = .nDownPaym
                                Case 4
                                    If .dTargetDt = "" Then
                                        loTxt.Text = ""
                                    Else
                                        loTxt.Text = Format(CDate(.dTargetDt), xsDATE_MEDIUM)
                                    End If
                                    loTxt.Tag = .dTargetDt
                                Case 5
                                    loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")
                                    loTxt.Tag = .sModelIDx
                                Case 6
                                    loTxt.Text = Format(CDbl(.nUnitPrce), xsDECIMAL)
                                    loTxt.Tag = .nUnitPrce
                                Case 7
                                    loTxt.Text = IIf(.nMonAmort = "", "N/A", Format(CDbl(.nMonAmort), xsDECIMAL))
                                    loTxt.Tag = .nMonAmort
                                Case 8
                                    loTxt.Text = IIf(.nAcctTerm = "", "N/A", .nAcctTerm + " Months")
                                    loTxt.Tag = .nAcctTerm
                                Case 9
                                    loTxt.Text = IIf(.nMonAmort = "", "N/A", Format(CDbl(.nMonAmort), xsDECIMAL))
                                    loTxt.Tag = .nMonAmort
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
                                    Case 0
                                        loTxt.Text = IIf(.sBranchCd = "", "N/A", p_oTrans.getBranch(.sBranchCd, False, True, ""))

                                    Case 3
                                        loTxt.Text = IIf(.nDownPaym = "", "N/A", Format(CDbl(.nDownPaym), xsDECIMAL))
                                    Case 4
                                        If .dTargetDt = "" Then
                                            loTxt.Text = "N/A"
                                        Else
                                            loTxt.Text = Format(CDate(.dTargetDt), xsDATE_MEDIUM)
                                        End If
                                    Case 5
                                        loTxt.Text = p_oTrans.getModel(.sModelIDx, False, True, "")

                                    Case 6
                                        loTxt.Text = IIf(.nUnitPrce = "", "N/A", Format(CDbl(.nUnitPrce), xsDECIMAL))
                                    Case 7
                                        loTxt.Text = IIf(.nMonAmort = "", "N/A", Format(CDbl(.nMonAmort), xsDECIMAL))
                                    Case 8
                                        loTxt.Text = IIf(.nAcctTerm = "", "N/A", .nAcctTerm + " Months")
                                    Case 9
                                        loTxt.Text = IIf(.nMonAmort = "", "N/A", Format(CDbl(.nMonAmort), xsDECIMAL))
                                End Select
                                If (.cUnitAppl <> "") Then setApplicationType(.cUnitAppl, cmb00, lb1Field02)
                                cmb00.Tag = .cUnitAppl
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


    Private Function DataComplete() As Boolean
        With p_oTrans.Category


        End With
        Return True
    End Function

    Private Sub isEntryOk()
        With p_oTrans.Category
            p_oTrans.Description.cUnitAppl = IIf(.cUnitAppl <> cmb00.Tag, "0", "1")
            p_oTrans.Description.cApplType = IIf(.cApplType <> cmb00.Tag, "0", "1")
            p_oTrans.Description.nDownPaym = IIf(.nDownPaym <> Double.Parse(txtIntro03.Tag), "0", "1")
            p_oTrans.Description.nAcctTerm = IIf(.nAcctTerm <> txtIntro08.Tag, "0", "1")
            p_oTrans.Description.nMonAmort = IIf(.nMonAmort <> Double.Parse(txtIntro09.Tag), "0", "1")
            p_oTrans.Description.sUnitAppl = IIf(.sUnitAppl <> txtIntro04.Tag, "0", "1")
            p_oTrans.Description.sModelIDx = IIf(.sModelIDx <> txtIntro05.Tag, "0", "1")
            p_oTrans.Description.sBranchCd = IIf(.sBranchCd <> txtIntro00.Tag, "0", "1")

            p_oTrans.Description.applicant_info.sLastName = IIf(.applicant_info.sLastName <> txtPerso00.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sFrstName = IIf(.applicant_info.sFrstName <> txtPerso01.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sMiddName = IIf(.applicant_info.sMiddName <> txtPerso02.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sSuffixNm = IIf(.applicant_info.sSuffixNm <> txtPerso03.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sBirthPlc = IIf(.applicant_info.sBirthPlc <> txtPerso06.Tag, "0", "1")
            p_oTrans.Description.applicant_info.cCvilStat = IIf(.applicant_info.cCvilStat <> cmb01.Tag, "0", "1")
            p_oTrans.Description.applicant_info.cGenderCd = IIf(.applicant_info.cGenderCd <> cmb02.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sMaidenNm = IIf(.applicant_info.sMaidenNm <> txtPerso06.Tag, "0", "1")
            p_oTrans.Description.applicant_info.facebook.sFBAcctxx = IIf(.applicant_info.facebook.sFBAcctxx <> txtPerso07.Tag, "0", "1")


            p_oTrans.Description.applicant_info.sLandMark = IIf(.applicant_info.sLandMark <> txtPerso07.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sHouseNox = IIf(.applicant_info.sHouseNox <> txtPerso08.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sAddress1 = IIf(.applicant_info.sAddress1 <> txtPerso09.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sAddress2 = IIf(.applicant_info.sAddress2 <> txtPerso10.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sTownIDxx = IIf(.applicant_info.sTownIDxx <> txtPerso11.Tag, "0", "1")
            p_oTrans.Description.applicant_info.sBrgyIDxx = IIf(.applicant_info.sBrgyIDxx <> txtPerso12.Tag, "0", "1")
          
        End With
    End Sub

End Class
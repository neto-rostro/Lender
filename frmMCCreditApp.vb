Imports Newtonsoft.Json
Imports ggcLRTransaction
Imports Newtonsoft.Json.Linq
Imports ggcGOCAS.CARConst
Imports ggcAppDriver

Public Class frmMCCreditApp
    Private lnMsg As String
    Private poControl As Control
    Private pnLoadx As Integer
    Private pnReference As Integer = 0
    Private pnEmail As Integer = 0
    Private pnSEmail As Integer = 0
    Private pnChild As Integer = 0
    Private pnMobile As Integer = 0
    Private pnSMobile As Integer = 0
    Private pnComakMobile As Integer = 0


    Private pxeModuleName As String = "E-Commerce MC Credit Application"
    Private WithEvents poTrans As ggcGOCAS.MCApplication

    Private Sub frmMCCrecitApp_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            poTrans = New ggcGOCAS.MCApplication(p_oAppDriver, 0)
            initDisplay()
            Call initTransaction()
            pnLoadx = 2
        End If
    End Sub

    Private Sub initTransaction()
        'initialize button depends on current mode
        initButton(0)

        'clear all text fields and lables
        Call ClearFields(Me.Panel1)
        Call ClearFields(Me.Panel2)

        'initialize grids
        Call initClientReference(dgvDetail01)
        Call initChildren(dgvDetail02)
        Call initNumber(dgvDetail03)
        Call initEmail(dgvDetail04)
        Call initSMobile(dgvDetail05)
        Call initEmail(dgvDetail06)
        Call initSMobile(dgvDetail07)

        'initialize listview
        Call initBank(listView00)
        Call initReference(listView01)
        Call initChildren(listView03)

        txtField90.Focus()
    End Sub

    Private Sub loadReference()
        Dim lsItems1 As ListViewItem
        Dim lsItems2 As ListViewItem

        Dim lsReference1(3) As String
        Dim lsReference2(3) As String

        lsReference1(0) = "1"
        lsReference1(1) = poTrans.Detail.sRefName1
        lsReference1(2) = poTrans.Detail.sRefAddr1
        lsItems1 = New ListViewItem(lsReference1)

        lsReference2(0) = "2"
        lsReference2(1) = poTrans.Detail.sRefName2
        lsReference2(2) = poTrans.Detail.sRefAddr2
        lsItems2 = New ListViewItem(lsReference2)

        With listView01
            .Items.Clear()
            .Items.Add(lsItems1)
            .Items.Add(lsItems2)
        End With
        Call loadReferenceCategory()
    End Sub

    Private Sub ShowReference(ByVal fnRow As Integer)
        With poTrans.Category.other_info
            If .personal_reference.Count = 0 Then Exit Sub
            txtRefer00.Text = .personal_reference(fnRow).sRefrNmex
            txtRefer01.Text = poTrans.getTownCity(.personal_reference(fnRow).sRefrAddx, False, True, "")
        End With
        pnReference = fnRow
    End Sub

    Private Sub loadReferenceCategory()
        With dgvDetail01
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
                .Rows(lnCtr).Cells(2).Value = poTrans.getTownCity(poTrans.Category.other_info.personal_reference(lnCtr).sRefrAddx, False, True, "")
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail01.CurrentCell = dgvDetail01.Rows(dgvDetail01.RowCount - 1).Cells(0)
        dgvDetail01.Rows(dgvDetail01.RowCount - 1).Selected = True
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

    Private Sub loadChilderen()
        Dim lsItems1 As ListViewItem
        Dim lsItems2 As ListViewItem
        Dim lsItems3 As ListViewItem

        Dim lsChildren1(4) As String
        Dim lsChildren2(4) As String
        Dim lsChildren3(4) As String

        lsChildren1(0) = "1"
        lsChildren1(1) = poTrans.Detail.sChldNme1
        lsChildren1(3) = poTrans.Detail.sChldSch1
        lsItems1 = New ListViewItem(lsChildren1)

        lsChildren2(0) = "2"
        lsChildren2(1) = poTrans.Detail.sChldNme2
        lsChildren2(2) = poTrans.Detail.sChldAge2
        lsChildren2(3) = poTrans.Detail.sChldSch2
        lsItems2 = New ListViewItem(lsChildren2)

        lsChildren3(0) = "3"
        lsChildren3(1) = poTrans.Detail.sChldNme3
        lsChildren3(2) = poTrans.Detail.sChldAge3
        lsChildren3(3) = poTrans.Detail.sChldSch3
        lsItems3 = New ListViewItem(lsChildren3)

        With listView03
            .Items.Clear()
            .Items.Add(lsItems1)
            .Items.Add(lsItems2)
            .Items.Add(lsItems3)
        End With
        Call loadChildrenCategory()
    End Sub

    Private Sub showChildren(ByVal fnRow As Integer)
        With poTrans.Category.disbursement_info.dependent_info
            If .children.Count = 0 Then Exit Sub
            txtChild00.Text = .children(fnRow).sFullName
            txtChild01.Text = .children(fnRow).nDepdAgex
            txtChild02.Text = .children(fnRow).sSchlName
        End With
        pnChild = fnRow
    End Sub

    Private Sub loadChildrenCategory()
        With dgvDetail02
            .Rows.Clear()
            If poTrans.Category.disbursement_info.dependent_info.children.Count = 0 Then
                poTrans.Category.disbursement_info.dependent_info.children.Add(New ggcGOCAS.GOCASConst.children_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.disbursement_info.dependent_info.children.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.disbursement_info.dependent_info.children(lnCtr).sFullName
                .Rows(lnCtr).Cells(2).Value = poTrans.Category.disbursement_info.dependent_info.children(lnCtr).nDepdAgex
                .Rows(lnCtr).Cells(3).Value = poTrans.Category.disbursement_info.dependent_info.children(lnCtr).sSchlName
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail02.CurrentCell = dgvDetail02.Rows(dgvDetail02.RowCount - 1).Cells(0)
        dgvDetail02.Rows(dgvDetail02.RowCount - 1).Selected = True
    End Sub

    Private Sub loadBanks()
        Dim lsItems1 As ListViewItem
        Dim lsItems2 As ListViewItem

        Dim lsBank1(4) As String
        Dim lsBank2(4) As String

        lsBank1(0) = "1"
        lsBank1(1) = poTrans.Detail.sBankNme1
        lsBank1(2) = poTrans.Detail.sBankBrh1
        lsBank1(3) = poTrans.Detail.sBankAcc1
        lsItems1 = New ListViewItem(lsBank1)

        lsBank2(0) = "2"
        lsBank2(1) = poTrans.Detail.sBankNme2
        lsBank2(2) = poTrans.Detail.sBankBrh2
        lsBank2(3) = poTrans.Detail.sBankAcc2
        lsItems2 = New ListViewItem(lsBank2)

        With listView00
            .Items.Clear()
            .Items.Add(lsItems1)
            .Items.Add(lsItems2)
        End With
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
            txtSpous06.Text = .mobile_number(fnRow).sMobileNo
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

    Private Sub cmb_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim loChk As ComboBox
        loChk = CType(sender, System.Windows.Forms.ComboBox)
        If e.KeyCode = Keys.Back Then

            Dim lnIndex As Integer
            With poTrans.Category
                lnIndex = Val(Mid(loChk.Name, 4))
                Select Case lnIndex
                    Case 0
                        .applicant_info.cGenderCd = ""
                        loChk.SelectedIndex = -1
                    Case 1
                        .applicant_info.cCvilStat = ""
                        loChk.SelectedIndex = -1
                    Case 2
                        .means_info.employed.cEmpSectr = ""
                        loChk.SelectedIndex = -1
                    Case 3
                        .means_info.employed.cEmpStatx = ""
                        loChk.SelectedIndex = -1
                    Case 4
                        .residence_info.cOwnershp = ""
                        loChk.SelectedIndex = -1
                    Case 5
                        .residence_info.cOwnOther = ""
                        loChk.SelectedIndex = -1
                    Case 6
                        .residence_info.rent_others.cRntOther = ""
                        loChk.SelectedIndex = -1
                    Case 7
                        .residence_info.cHouseTyp = ""
                        loChk.SelectedIndex = -1
                    Case 8
                        .residence_info.cGaragexx = ""
                        loChk.SelectedIndex = -1
                    Case 9
                        .spouse_means.employed.cEmpStatx = ""
                        loChk.SelectedIndex = -1
                    Case 10
                        .spouse_info.residence_info.cOwnershp = ""
                        loChk.SelectedIndex = -1
                    Case 11
                        .spouse_info.residence_info.cOwnOther = ""
                        loChk.SelectedIndex = -1
                    Case 12
                        .spouse_info.residence_info.rent_others.cRntOther = ""
                        loChk.SelectedIndex = -1
                    Case 13
                        .spouse_info.residence_info.cHouseTyp = ""
                        loChk.SelectedIndex = -1
                    Case 14
                        .spouse_info.residence_info.cGaragexx = ""
                        loChk.SelectedIndex = -1
                    Case 15
                        .applicant_info.mobile_number(pnMobile).cPostPaid = ""
                        loChk.SelectedIndex = -1
                    Case 16
                        .comaker_info.sReltnCde = ""
                        loChk.SelectedIndex = -1
                    Case 17
                        .means_info.cIncmeSrc = ""
                        loChk.SelectedIndex = -1
                    Case 18
                        .means_info.employed.cEmpSectr = ""
                        loChk.SelectedIndex = -1
                    Case 19
                        .means_info.employed.cUniforme = ""
                        loChk.SelectedIndex = -1
                    Case 20
                        .means_info.employed.cMilitary = ""
                        loChk.SelectedIndex = -1
                    Case 21
                        .means_info.employed.cGovtLevl = ""
                        loChk.SelectedIndex = -1
                    Case 22
                        .means_info.employed.cCompLevl = ""
                        loChk.SelectedIndex = -1
                    Case 23
                        .means_info.employed.cEmpLevlx = ""
                        loChk.SelectedIndex = -1
                    Case 24
                        .means_info.employed.cOcCatgry = ""
                        loChk.SelectedIndex = -1
                    Case 25
                        .means_info.employed.cOFWRegnx = ""
                        loChk.SelectedIndex = -1
                    Case 26
                        .disbursement_info.bank_account.sAcctType = ""
                        loChk.SelectedIndex = -1
                    Case 28
                        .cApplType = ""
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
                End Select
            End With
        End If
    End Sub

    Private Sub combobox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As ComboBox
        loChk = CType(sender, System.Windows.Forms.ComboBox)

        Dim lnIndex As Integer
        With poTrans.Category
            lnIndex = Val(Mid(loChk.Name, 4))
            Select Case lnIndex
                Case 0
                    .applicant_info.cGenderCd = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 1
                    .applicant_info.cCvilStat = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 2
                    .means_info.employed.cEmpSectr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 3
                    .means_info.employed.cEmpStatx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 4
                    .residence_info.cOwnershp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 5
                    .residence_info.cOwnOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 6
                    .residence_info.rent_others.cRntOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 7
                    .residence_info.cHouseTyp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 8
                    .residence_info.cGaragexx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 9
                    .spouse_means.employed.cEmpStatx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 10
                    .spouse_info.residence_info.cOwnershp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 11
                    .spouse_info.residence_info.cOwnOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 12
                    .spouse_info.residence_info.rent_others.cRntOther = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 13
                    .spouse_info.residence_info.cHouseTyp = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 14
                    .spouse_info.residence_info.cGaragexx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 15
                    .applicant_info.mobile_number(pnMobile).cPostPaid = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 16
                    .comaker_info.sReltnCde = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 17
                    .means_info.cIncmeSrc = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 18
                    .means_info.employed.cEmpSectr = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 19
                    .means_info.employed.cUniforme = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 20
                    .means_info.employed.cMilitary = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 21
                    .means_info.employed.cGovtLevl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 22
                    .means_info.employed.cCompLevl = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 23
                    .means_info.employed.cEmpLevlx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 24
                    .means_info.employed.cOcCatgry = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 25
                    .means_info.employed.cOFWRegnx = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 26
                    .disbursement_info.bank_account.sAcctType = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
                Case 28
                    .cApplType = IIf(loChk.SelectedIndex.ToString = "-1", "", loChk.SelectedIndex.ToString)
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
            End Select
        End With
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 'close
                Me.Close()
            Case 1 'cancel edit
                lnMsg = MsgBox("Cancel Edit? " & _
                               vbCrLf + "All Changes will not save??", vbYesNo + vbInformation, "Confirm")
                If lnMsg = vbYes Then
                    ClearFields(Me.Panel1)
                    ClearFields(Me.Panel2)
                    initButton(0)
                End If
            Case 4 'save
                If poTrans.Category.applicant_info.cCvilStat <> "1" And poTrans.Category.applicant_info.cCvilStat <> "5" Then
                    If isWithSpouse(grp09) = False Then Exit Sub
                    If isWithSpouse(grp11) = False Then Exit Sub
                    If isWithSpouse(grp13) = False Then Exit Sub
                End If

                If isEntryOk() = False Then Exit Sub
                If MsgBox("Do you want to save this application??", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                    poTrans.GenerateQM()
                    If poTrans.confirmTransaction Then
                        MsgBox("Application Successfully Save!!", vbInformation, "Information")
                        Call ClearFields(Me.Panel1)
                        Call ClearFields(Me.Panel2)
                        poTrans.OpenTransaction(poTrans.Master("sTransNox"))
                        loadTransaction()
                        initButton(0)
                    End If
                End If

            Case 3 'browse transaction
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
            Case 8 'call customer
                If txtField00.Text <> "" Then
                    If IsDBNull(poTrans.Master("sCatInfox")) Then GoTo moveTo
                    If Not poTrans.Master("sCatInfox").Equals("") Then
                        MsgBox("The customer was already called. Please click call by reference instead...", vbCritical, "Error")
                        Exit Sub
                    End If
moveTo:
                    displayMobile(poTrans.callApplicant)
                    poTrans.OpenTransaction(poTrans.Master("sTransNox"))
                    initButton(1)
                End If

            Case 12 'add reference
                If Trim(dgvDetail01.Rows(dgvDetail01.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.other_info.personal_reference.Add(New ggcGOCAS.GOCASConst.personal_reference_param)
                    dgvDetail01.Rows.Add()
                    Call loadReferenceCategory()
                    dgvDetail01.CurrentCell = dgvDetail01(0, Me.dgvDetail01.RowCount - 1)
                    dgvDetail01_Click(sender, New System.EventArgs())
                End If

            Case 13 'delete Reference
                If dgvDetail01.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.other_info.personal_reference.RemoveAt(pnReference)
                        dgvDetail01.CurrentCell = dgvDetail01(0, 0)
                        dgvDetail01_Click(sender, New System.EventArgs())
                        Call loadReferenceCategory()
                    End If
                End If

            Case 14 'add children
                If Trim(dgvDetail02.Rows(dgvDetail02.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.disbursement_info.dependent_info.children.Add(New ggcGOCAS.GOCASConst.children_param)
                    dgvDetail02.Rows.Add()
                    Call loadChildrenCategory()
                    dgvDetail02.CurrentCell = dgvDetail02(0, Me.dgvDetail02.RowCount - 1)
                    dgvDetail02_Click(sender, New System.EventArgs())
                End If

            Case 15 'delete children
                If dgvDetail02.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.disbursement_info.dependent_info.children.RemoveAt(pnChild)
                        dgvDetail02.CurrentCell = dgvDetail02(0, 0)
                        dgvDetail02_Click(sender, New System.EventArgs())
                        Call loadChildrenCategory()
                    End If
                End If

            Case 16 'add mobile
                If Trim(dgvDetail03.Rows(dgvDetail03.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.applicant_info.mobile_number.Add(New ggcGOCAS.GOCASConst.mobileno_param)
                    dgvDetail03.Rows.Add()
                    Call loadAppliMobile()
                    dgvDetail03.CurrentCell = dgvDetail03(0, Me.dgvDetail03.RowCount - 1)
                    dgvDetail03_Click(sender, New System.EventArgs())
                End If

            Case 17 'delete mobile
                If dgvDetail03.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.applicant_info.mobile_number.RemoveAt(pnMobile)
                        dgvDetail03.CurrentCell = dgvDetail03(0, 0)
                        dgvDetail03_Click(sender, New System.EventArgs())
                        Call loadAppliMobile()
                    End If
                End If
            Case 18 'add email
                If Trim(dgvDetail04.Rows(dgvDetail04.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.applicant_info.email_address.Add(New ggcGOCAS.GOCASConst.email_param)
                    dgvDetail04.Rows.Add()
                    Call loadAppliEmail()
                    dgvDetail04.CurrentCell = dgvDetail04(0, Me.dgvDetail04.RowCount - 1)
                    dgvDetail04_Click(sender, New System.EventArgs())
                End If

            Case 19 'delete email
                If dgvDetail04.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.applicant_info.email_address.RemoveAt(pnEmail)
                        dgvDetail04.CurrentCell = dgvDetail04(0, 0)
                        dgvDetail04_Click(sender, New System.EventArgs())
                        Call loadAppliEmail()
                    End If
                End If
            Case 20 'add spouse mobile
                If Trim(dgvDetail05.Rows(dgvDetail05.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.spouse_info.personal_info.mobile_number.Add(New ggcGOCAS.GOCASConst.mobileno_param)
                    dgvDetail05.Rows.Add()
                    Call loadSpouseMobile()
                    dgvDetail05.CurrentCell = dgvDetail05(0, Me.dgvDetail05.RowCount - 1)
                    dgvDetail05_Click(sender, New System.EventArgs())
                End If

            Case 21 'delete spouse mobile
                If dgvDetail05.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.spouse_info.personal_info.mobile_number.RemoveAt(pnSMobile)
                        dgvDetail05.CurrentCell = dgvDetail05(0, 0)
                        dgvDetail05_Click(sender, New System.EventArgs())
                        Call loadSpouseMobile()
                    End If
                End If

            Case 22 'add spouse email
                If Trim(dgvDetail06.Rows(dgvDetail06.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.spouse_info.personal_info.email_address.Add(New ggcGOCAS.GOCASConst.email_param)
                    dgvDetail06.Rows.Add()
                    Call loadSpouseEmail()
                    dgvDetail06.CurrentCell = dgvDetail06(0, Me.dgvDetail06.RowCount - 1)
                    dgvDetail06_Click(sender, New System.EventArgs())
                End If

            Case 23 'delete spouse email
                If dgvDetail06.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.spouse_info.personal_info.email_address.RemoveAt(pnSEmail)
                        dgvDetail06.CurrentCell = dgvDetail06(0, 0)
                        dgvDetail06_Click(sender, New System.EventArgs())
                        Call loadSpouseEmail()
                    End If
                End If

            Case 24 'add comak mobile
                If Trim(dgvDetail07.Rows(dgvDetail07.Rows.Count - 1).Cells(1).Value) <> "" Then
                    poTrans.Category.comaker_info.mobile_number.Add(New ggcGOCAS.GOCASConst.mobileno_param)
                    dgvDetail07.Rows.Add()
                    Call loadComakMobile()
                    dgvDetail07.CurrentCell = dgvDetail07(0, Me.dgvDetail07.RowCount - 1)
                    dgvDetail07_Click(sender, New System.EventArgs())
                End If

            Case 25 'delete comak mobile
                If dgvDetail07.RowCount - 1 > 0 Then
                    If MsgBox("Do you really want to delete this data?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirmation") = MsgBoxResult.Ok Then
                        poTrans.Category.comaker_info.mobile_number.RemoveAt(pnComakMobile)
                        dgvDetail07.CurrentCell = dgvDetail07(0, 0)
                        dgvDetail07_Click(sender, New System.EventArgs())
                        Call loadComakMobile()
                    End If
                End If
        End Select
    End Sub

    Private Sub initDisplay()
        With listItem
            .Columns.Clear()
            .Items.Clear()

            listItem.View = View.Details
            .Columns.Add("Number", 80, HorizontalAlignment.Left)
            .Columns.Add("Name", 100, HorizontalAlignment.Left)
        End With
    End Sub

    Private Sub displayMobile(ByVal fsValue As String)
        Dim listOfMobile As String
        listOfMobile = fsValue
        With listItem
            .Items.Clear()
            .Items.Add(listOfMobile).SubItems.Add(poTrans.Detail.sFrstName)
        End With
    End Sub

    Private Function isEntryOk() As Boolean
        With poTrans.Category
            If .sModelIDx = "" Then
                MessageBox.Show("No Model Entry Detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtField06.Focus()
                Return False
            ElseIf .applicant_info.sLastName = "" Then
                MessageBox.Show("No Applicant Last Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli01.Focus()
                Return False
            ElseIf .applicant_info.sFrstName = "" Then
                MessageBox.Show("No Applicant First Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli00.Focus()
                Return False
            ElseIf .applicant_info.sMiddName = "" Then
                MessageBox.Show("No Applicant Middle Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli02.Focus()
                Return False
            ElseIf Not IsDate(.applicant_info.dBirthDte) Then
                MessageBox.Show("Invalid Birth Date entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli07.Focus()
                Return False
            ElseIf .applicant_info.sBirthPlc = "" Or txtAppli08.Text = "" Then
                MessageBox.Show("No Birth Place entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli08.Focus()
                Return False
            ElseIf .applicant_info.cCvilStat = "" Then
                MessageBox.Show("No Civil Status Entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                cmb01.Focus()
                Return False
            ElseIf .applicant_info.cGenderCd = "" Then
                MessageBox.Show("No Gender Entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                cmb00.Focus()
                Return False
            ElseIf .applicant_info.sCitizenx = "" Then
                MessageBox.Show("No Citizenship Entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli11.Focus()
                Return False
            ElseIf .applicant_info.mobile_number.Count < 0 Then
                MessageBox.Show("No Mobile No Detected!", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 0
                txtAppli05.Focus()
                Return False
            ElseIf .residence_info.present_address.sAddress1 = "" And .residence_info.present_address.sAddress2 = "" Then
                MessageBox.Show("No present Phase #/Lot #/Sitio or Street name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtApRes03.Focus()
                Return False
            ElseIf .residence_info.present_address.sTownIDxx = "" Then
                MessageBox.Show("No present Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtApRes04.Focus()
                Return False
            ElseIf .residence_info.present_address.sBrgyIDxx = "" Then
                MessageBox.Show("No present barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtApRes05.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sAddress1 = "" And .residence_info.permanent_address.sAddress2 = "" Then
                MessageBox.Show("No permanent Phase #/Lot #/Sitio or Street name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtApRes12.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sTownIDxx = "" Then
                MessageBox.Show("No permanent Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtApRes13.Focus()
                Return False
            ElseIf .residence_info.permanent_address.sBrgyIDxx = "" Then
                MessageBox.Show("No permanent barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tabControl00.SelectedIndex = 1
                txtApRes14.Focus()
                Return False
            End If

            If .applicant_info.cCvilStat = "1" Or .applicant_info.cCvilStat = "5" Then
                If .spouse_info.personal_info.sLastName = "" Then
                    MessageBox.Show("No Spouse Last Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 5
                    txtSpous02.Focus()
                    Return False
                ElseIf .spouse_info.personal_info.sFrstName = "" Then
                    MessageBox.Show("No Spouse First Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 5
                    txtSpous00.Focus()
                    Return False
                ElseIf .spouse_info.personal_info.sMiddName = "" Then
                    MessageBox.Show("No Spouse Middle Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 5
                    txtSpous01.Focus()
                    Return False
                ElseIf Not IsDate(.spouse_info.personal_info.dBirthDte) Then
                    MessageBox.Show("Invalid Spouse Birth date entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 5
                    txtSpous08.Focus()
                    Return False
                ElseIf .spouse_info.personal_info.sBirthPlc = "" Or txtSpous09.Text = "" Then
                    MessageBox.Show("No Spouse Birth Place entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 5
                    txtSpous09.Focus()
                    Return False
                ElseIf .spouse_info.personal_info.sCitizenx = "" Then
                    MessageBox.Show("No Citizenship Entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 5
                    txtSpous10.Focus()
                    Return False
                ElseIf .spouse_info.residence_info.present_address.sAddress1 = "" And .spouse_info.residence_info.present_address.sAddress2 = "" Then
                    MessageBox.Show("No Spouse Present Phase #/Lot #/Sitio or Street Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 6
                    txtResid03.Focus()
                    Return False
                ElseIf .spouse_info.residence_info.present_address.sTownIDxx = "" Then
                    MessageBox.Show("No Spouse present Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 6
                    txtResid04.Focus()
                    Return False
                ElseIf .spouse_info.residence_info.present_address.sBrgyIDxx = "" Then
                    MessageBox.Show("No Spouse present barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 6
                    txtResid05.Focus()
                    Return False
                ElseIf .spouse_info.residence_info.permanent_address.sAddress1 = "" And .spouse_info.residence_info.permanent_address.sAddress2 = "" Then
                    MessageBox.Show("No Spouse Permanent Phase #/Lot #/Sitio or Street Name entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 6
                    txtResid12.Focus()
                    Return False
                ElseIf .spouse_info.residence_info.permanent_address.sTownIDxx = "" Then
                    MessageBox.Show("No Spouse permanent Town/City entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 6
                    txtResid13.Focus()
                    Return False
                ElseIf .spouse_info.residence_info.permanent_address.sBrgyIDxx = "" Then
                    MessageBox.Show("No Spouse permanent barangay entry detected", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tabControl00.SelectedIndex = 6
                    txtResid14.Focus()
                    Return False
                End If
            End If
        End With

        Return True
    End Function

    Public Function isWithSpouse(ByVal groupbox As GroupBox) As Boolean
        Dim loTxt As Control
        For Each loTxt In groupbox.Controls
            If TypeOf loTxt Is TextBox Then
                If loTxt.Text <> "" Then
                    Dim ans As String
                    ans = MsgBox("Selected customer's civil status doesnt require spouse information!" & vbCrLf & _
                                 "By proceeding all info of spouse will be remove...", vbCritical + vbYesNo, "Confirm")
                    If ans = vbYes Then
                        ClearSpouseInfo(grp09)
                        ClearSpouseInfo(grp11)
                        ClearSpouseInfo(grpBox18)
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

    Private Sub clearSpouseDetails()
        With poTrans.Category
            .disbursement_info.dependent_info.children.Clear()
            .spouse_info.personal_info.sFrstName = ""
            .spouse_info.personal_info.sMiddName = ""
            .spouse_info.personal_info.sLastName = ""
            .spouse_info.personal_info.sSuffixNm = ""
            .spouse_info.personal_info.sNickName = ""
            .spouse_info.personal_info.mobile_number.Clear()
            .spouse_info.personal_info.email_address.Clear()
            .spouse_info.personal_info.dBirthDte = ""
            .spouse_info.personal_info.sBirthPlc = ""
            .spouse_means.employed.sEmployer =
            .spouse_means.employed.sWrkTownx = ""
            .spouse_means.employed.sWrkTelno = ""
            .spouse_means.employed.nLenServc = ""
            .spouse_means.employed.nSalaryxx = ""
            .spouse_means.employed.cEmpStatx = ""
            .spouse_means.employed.sPosition = ""
            .spouse_means.self_employed.sIndstBus = ""
            .spouse_means.self_employed.sBusTownx = ""
            .spouse_means.self_employed.nBusIncom = ""
            .spouse_means.self_employed.nBusLenxx = ""
            .spouse_means.other_income.sOthrIncm = ""
            .spouse_info.residence_info.present_address.sLandMark = ""
            .spouse_info.residence_info.present_address.sHouseNox = ""
            .spouse_info.residence_info.present_address.sAddress1 = ""
            .spouse_info.residence_info.present_address.sAddress2 = ""
            .spouse_info.residence_info.present_address.sTownIDxx = ""
            .spouse_info.residence_info.present_address.sBrgyIDxx = ""
            .spouse_info.residence_info.cOwnershp = ""
            .spouse_info.residence_info.cOwnOther = ""
            .spouse_info.residence_info.rent_others.cRntOther = ""
            .spouse_info.residence_info.rent_others.nLenStayx = ""
            .spouse_info.residence_info.rent_others.nRentExps = ""
            .spouse_info.residence_info.sCtkReltn = ""
            .spouse_info.residence_info.cHouseTyp = ""
            .spouse_info.residence_info.cGaragexx = ""
            .spouse_info.residence_info.permanent_address.sHouseNox = ""
            .spouse_info.residence_info.permanent_address.sAddress1 = ""
            .spouse_info.residence_info.permanent_address.sAddress2 = ""
            .spouse_info.residence_info.permanent_address.sTownIDxx = ""
            .spouse_info.residence_info.permanent_address.sBrgyIDxx = ""
            Call loadSpouseMobile()
            Call loadSpouseEmail()
            Call loadChildrenCategory()
        End With
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtSEmpl" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "txtChild" Then
                        Select Case loIndex
                            Case Else
                                loTxt.Text = ""
                        End Select
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub frmMCCrecitApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtAppli", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtAppli", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtAppli", "Validating", AddressOf txtAppli_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtAppli", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtApRes", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtApRes", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtApRes", "Validating", AddressOf txtApRes_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtApRes", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtEmplo", "Validating", AddressOf txtEmplo_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtEmplo", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtSpous", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpous", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSpous", "Validating", AddressOf txtSpous_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSpous", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtResid", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtResid", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtResid", "Validating", AddressOf txtResid_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtResid", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtSEmpl", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSEmpl", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSEmpl", "Validating", AddressOf txtSEmpl_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSEmpl", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtBanks", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtBanks", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtBanks", "Validating", AddressOf txtBanks_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtBanks", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtChild", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtChild", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtChild", "Validating", AddressOf txtChild_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtChild", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtComak", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtComak", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtComak", "Validating", AddressOf txtComak_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtComak", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtOther", "Validating", AddressOf txtOther_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtOther", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtRefer", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtRefer", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtRefer", "Validating", AddressOf txtRefer_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtRefer", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            Call grpKeyHandler(Me, GetType(ComboBox), "cmb", "KeyDown", AddressOf cmb_KeyDown)
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
                            poTrans.OpenTransaction(poTrans.Master("sTransNox"))
                            Call loadTransaction()
                            txtField90.Text = poTrans.Master("sTransNox")
                            txtField91.Text = poTrans.Master("sClientNm")
                        Else
                            Call ClearFields(Me.Panel1)
                            Call ClearFields(Me.Panel2)
                        End If
                    Case 5
                        loTxt.Text = poTrans.getBranch(loTxt.Text, True, False, poTrans.Master("sBranchCd"))
                    Case 6
                        loTxt.Text = poTrans.getModel(loTxt.Text, True, False, poTrans.Category.sModelIDx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtAppli" Then
                Select Case loIndex
                    Case 8
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.applicant_info.sBirthPlc)
                    Case 11
                        loTxt.Text = poTrans.getCountry(loTxt.Text, True, False, poTrans.Category.applicant_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtEmplo" Then
                Select Case loIndex
                    Case 1
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.means_info.employed.sWrkTownx)
                    Case 5
                        loTxt.Text = poTrans.getOccupation(loTxt.Text, True, False, poTrans.Category.means_info.employed.sPosition)
                    Case 7
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.means_info.self_employed.sBusTownx)
                    Case 12
                        loTxt.Text = poTrans.getCountry(loTxt.Text, True, False, poTrans.Category.means_info.employed.sOFWNatnx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtApRes" Then
                Select Case loIndex
                    Case 4
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.residence_info.present_address.sTownIDxx)
                    Case 5
                        loTxt.Text = poTrans.getBarangay(loTxt.Text, True, False, poTrans.Category.residence_info.present_address.sBrgyIDxx, poTrans.Category.residence_info.present_address.sTownIDxx)
                    Case 13
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.residence_info.permanent_address.sTownIDxx)
                    Case 14
                        loTxt.Text = poTrans.getBarangay(loTxt.Text, True, False, poTrans.Category.residence_info.permanent_address.sBrgyIDxx, poTrans.Category.residence_info.permanent_address.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtRefer" Then
                Select Case loIndex
                    Case 1
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.other_info.personal_reference(pnReference).sRefrAddx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSpous" Then
                Select Case loIndex
                    Case 9
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.spouse_info.personal_info.sBirthPlc)
                    Case 10
                        loTxt.Text = poTrans.getCountry(loTxt.Text, True, False, poTrans.Category.spouse_info.personal_info.sCitizenx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtSEmpl" Then
                Select Case loIndex
                    Case 1
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.spouse_means.employed.sWrkTownx)
                    Case 5
                        loTxt.Text = poTrans.getOccupation(loTxt.Text, True, False, poTrans.Category.spouse_means.employed.sPosition)
                    Case 7
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.spouse_means.self_employed.sBusTownx)
                    Case 12
                        loTxt.Text = poTrans.getCountry(loTxt.Text, True, False, poTrans.Category.spouse_means.employed.sOFWNatnx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtResid" Then
                Select Case loIndex
                    Case 4
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.spouse_info.residence_info.present_address.sTownIDxx)
                    Case 5
                        loTxt.Text = poTrans.getBarangay(loTxt.Text, True, False, poTrans.Category.spouse_info.residence_info.present_address.sBrgyIDxx, poTrans.Category.spouse_info.residence_info.present_address.sTownIDxx)
                    Case 13
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.spouse_info.residence_info.permanent_address.sTownIDxx)
                    Case 14
                        loTxt.Text = poTrans.getBarangay(loTxt.Text, True, False, poTrans.Category.spouse_info.residence_info.permanent_address.sBrgyIDxx, poTrans.Category.spouse_info.residence_info.permanent_address.sTownIDxx)
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtComak" Then
                Select Case loIndex
                    Case 3
                        loTxt.Text = poTrans.getTownCity(loTxt.Text, True, False, poTrans.Category.comaker_info.sBirthPlc)
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
                    If LCase(Mid(loTxt.Name, 1, 8)) = "lblField" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblEmplo" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblSpous" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblSpoRe" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblApRes" Or _
                        LCase(Mid(loTxt.Name, 1, 8)) = "lblSEmpl" Or _
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
                        If LCase(Mid(loTxt.Name, 1, 8)) = "txtAppli" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtApRes" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtSEmpl" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtResid" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtChild" Or _
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
        pnChild = 0
        pnMobile = 0
        pnEmail = 0
        pnSEmail = 0
        pnSMobile = 0
        pnComakMobile = 0
        setTranStat(-1, lblStatus)
        tabControl00.SelectedIndex = 0
        txtAppli00.Focus()
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
                                If Not IsDate(.Master("dTransact")) Then .Master("dTransact") = p_oAppDriver.getSysDate
                                loTxt.Text = Format(.Master("dTransact"), xsDATE_MEDIUM)
                            Case 2
                                loTxt.Text = .Master("sClientNm")
                            Case 81
                                loTxt.Text = .Detail.sPresAddr
                            Case 3
                                If Not IsDate(.Detail.dBirthDte) Then .Detail.dBirthDte = p_oAppDriver.getSysDate
                                loTxt.Text = Format(CDate(.Detail.dBirthDte), xsDATE_MEDIUM)
                            Case 4
                                If Not IsNumeric(.Detail.nAgexxxxx) Then .Detail.nAgexxxxx = 0
                                loTxt.Text = CInt(.Detail.nAgexxxxx)
                            Case 5
                                loTxt.Text = poTrans.getBranch(poTrans.Master("sBranchCd"), False, True, "")
                            Case 6
                                loTxt.Text = poTrans.getModel(.Category.sModelIDx, False, True, "")
                            Case 7
                                If Not IsNumeric(.Category.nAcctTerm) Then .Category.nAcctTerm = 0
                                loTxt.Text = CInt(.Category.nAcctTerm)
                            Case 8
                                If Not IsNumeric(.Detail.nDownPaym) Then .Category.nDownPaym = 0
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
            ElseIf (TypeOf loTxt Is Label) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "lblField" Then
                    With poTrans.Detail
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sFrstName
                            Case 1
                                loTxt.Text = .sLastName
                            Case 2
                                loTxt.Text = .sMiddName
                            Case 3
                                loTxt.Text = .sSuffixNm
                            Case 4
                                loTxt.Text = .sNickName
                            Case 5
                                loTxt.Text = .sGenderxx
                            Case 6
                                loTxt.Text = .sCvilStat
                            Case 7
                                loTxt.Text = .sMobileNo
                            Case 8
                                loTxt.Text = .sEmailAdd
                            Case 9
                                If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                            Case 10
                                loTxt.Text = .sBrtPlace
                            Case 12
                                loTxt.Text = .sMotherNm
                                'Case 13
                                '    loTxt.Text = .s
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtAppli" Then
                    With poTrans.Category.applicant_info
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sFrstName
                            Case 1
                                loTxt.Text = .sLastName
                            Case 2
                                loTxt.Text = .sMiddName
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
                            Case 11
                                loTxt.Text = poTrans.getCountry(.sCitizenx, False, True, "")
                        End Select
                        If (.cGenderCd <> "") Then setGender(.cGenderCd, cmb00)
                        If (.cCvilStat <> "") Then setCivilStat(.cCvilStat, cmb01)
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub loadAppEmployment(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadAppEmployment(loTxt)
            ElseIf (TypeOf loTxt Is Label) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "lblEmplo" Then
                    With poTrans.Detail
                        Select Case loIndex
                            Case 15
                                loTxt.Text = .sEmplType
                            Case 1
                                loTxt.Text = .sCompnyNm
                            Case 2
                                loTxt.Text = .sCompnyAd
                            Case 3
                                loTxt.Text = .sCompTele
                            Case 4
                                loTxt.Text = .sLenServe
                            Case 5
                                loTxt.Text = .sGrIncome
                            Case 6
                                loTxt.Text = .sEmplStat
                            Case 7
                                loTxt.Text = .sEmpPostn
                            Case 8
                                loTxt.Text = .sBusiness
                            Case 9
                                loTxt.Text = .sBusiAddr
                            Case 11
                                loTxt.Text = .sBusIncom
                            Case 12
                                loTxt.Text = .sYrInBusi
                            Case 13
                                loTxt.Text = .sSourceIn
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtEmplo" Then
                    With poTrans.Category.means_info
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .employed.sEmployer
                            Case 1
                                loTxt.Text = poTrans.getTownCity(.employed.sWrkAddrx, False, True, "")
                            Case 2
                                loTxt.Text = .employed.sWrkTelno
                            Case 3
                                If Not IsNumeric(.employed.nLenServc) Then .employed.nLenServc = 0
                                loTxt.Text = CInt(.employed.nLenServc)
                            Case 4
                                If Not IsNumeric(.employed.nSalaryxx) Then .employed.nSalaryxx = 0
                                loTxt.Text = FormatNumber(CDbl(.employed.nSalaryxx), 2)
                            Case 5
                                loTxt.Text = poTrans.getOccupation(.employed.sPosition, False, True, "")
                            Case 6
                                loTxt.Text = .self_employed.sIndstBus
                            Case 7
                                loTxt.Text = poTrans.getTownCity(.self_employed.sBusAddrx, False, True, "")
                            Case 9
                                If Not IsNumeric(.self_employed.nBusIncom) Then .self_employed.nBusIncom = 0
                                loTxt.Text = FormatNumber(CDbl(.self_employed.nBusIncom), 2)
                            Case 10
                                If Not IsNumeric(.self_employed.nBusLenxx) Then .self_employed.nBusLenxx = 0
                                loTxt.Text = CInt(.self_employed.nBusLenxx)
                            Case 11
                                loTxt.Text = .other_income.nOthrIncm
                        End Select
                        If (.employed.cEmpStatx <> "") Then setStatEmployment(.employed.cEmpStatx, cmb03)

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
        Next
    End Sub

    Private Sub loadSpouseInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseInfo(loTxt)
            ElseIf (TypeOf loTxt Is Label) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "lblSpous" Then
                    With poTrans.Detail
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sSpFrstNm
                            Case 1
                                loTxt.Text = .sSpMiddNm
                            Case 2
                                loTxt.Text = .sSpLastNm
                            Case 3
                                loTxt.Text = .sSpSuffNm
                            Case 4
                                loTxt.Text = .sSpNickNm
                            Case 5
                                If Not IsNumeric(.nSpAgexxx) Then .nSpAgexxx = 0
                                loTxt.Text = CInt(.nSpAgexxx)
                            Case 6
                                loTxt.Text = .sSpMobiNo
                            Case 7
                                loTxt.Text = .sSpEmailx
                            Case 8
                                If Not IsDate(.dSpBrtDte) Then .dSpBrtDte = p_oAppDriver.getSysDate
                                loTxt.Text = Format(CDate(.dSpBrtDte), xsDATE_MEDIUM)
                            Case 9
                                loTxt.Text = .sSpBrtPlc
                                'Case 10
                                '    loTxt.Text = poTrans.getCountry(.sBirthPlc, False, True, "")
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Then
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
                            Case 8
                                If Not IsDate(.dBirthDte) Then .dBirthDte = p_oAppDriver.getSysDate
                                loTxt.Text = Format(CDate(.dBirthDte), xsDATE_MEDIUM)
                            Case 9
                                loTxt.Text = poTrans.getTownCity(.sBirthPlc, False, True, "")
                            Case 10
                                loTxt.Text = poTrans.getCountry(.sCitizenx, False, True, "")
                        End Select
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub loadComaker(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadComaker(loTxt)
            ElseIf (TypeOf loTxt Is Label) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "lblComak" Then
                    With poTrans.Detail
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sCLFrstNm
                            Case 1
                                loTxt.Text = .sCLMiddNm
                            Case 2
                                loTxt.Text = .sCLLastNm
                            Case 3
                                loTxt.Text = .sCLAddres
                            Case 4
                                loTxt.Text = .sCLRelatn
                            Case 6
                                loTxt.Text = .sCLContct
                            Case 8
                                If Not IsDate(.sCLBrtDte) Then .sCLBrtDte = p_oAppDriver.getSysDate
                                loTxt.Text = Format(CDate(.sCLBrtDte), xsDATE_MEDIUM)
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
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
                        If (.sReltnCde <> "") Then setFinanceType(.sReltnCde, cmb16)
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub loadOther(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadOther(loTxt)
            ElseIf (TypeOf loTxt Is Label) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "lblOther" Then
                    With poTrans.Detail
                        Select Case loIndex
                            Case 0
                                If Not IsNumeric(.sRentalxx) Then .sRentalxx = 0
                                loTxt.Text = FormatNumber(CDbl(.sRentalxx), 2)
                            Case 1
                                If Not IsNumeric(.sElectric) Then .sElectric = 0
                                loTxt.Text = FormatNumber(CDbl(.sElectric), 2)
                            Case 2
                                If Not IsNumeric(.sWaterBil) Then .sWaterBil = 0
                                loTxt.Text = FormatNumber(CDbl(.sWaterBil), 2)
                            Case 3
                                If Not IsNumeric(.sOthrLoan) Then .sOthrLoan = 0
                                loTxt.Text = FormatNumber(CDbl(.sOthrLoan), 2)
                            Case 4
                                loTxt.Text = .sCredtCrd
                            Case 5
                                If Not IsNumeric(.sCredtLmt) Then .sCredtLmt = 0
                                loTxt.Text = FormatNumber(CDbl(.sCredtLmt), 2)
                            Case 6
                                loTxt.Text = .sEducAttn
                            Case 7
                                loTxt.Text = .sLandmark
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtOther" Then
                    With poTrans.Category.disbursement_info
                        Select Case loIndex
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
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub loadSpouseEmployment(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseEmployment(loTxt)
            ElseIf (TypeOf loTxt Is Label) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "lblSEmpl" Then
                    With poTrans.Detail
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sSpCompNm
                            Case 1
                                loTxt.Text = .sSpCompAd
                            Case 2
                                loTxt.Text = .sSpComTel
                            Case 3
                                loTxt.Text = .sSpLenSrv
                            Case 4
                                loTxt.Text = .sSpMonPay
                            Case 5
                                loTxt.Text = .sSpEmpSta
                            Case 6
                                loTxt.Text = .sSpEmpPos
                            Case 7
                                loTxt.Text = .sSpBusins
                            Case 8
                                loTxt.Text = .sSpBusiAd
                            Case 9
                                loTxt.Text = .sSpBusTel
                            Case 10
                                loTxt.Text = .sSpBusInc
                            Case 11
                                loTxt.Text = .sSpYrsBus
                            Case 12
                                loTxt.Text = .sSpSrcInc
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtSEmpl" Then
                    With poTrans.Category.spouse_means
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .employed.sEmployer
                            Case 1
                                loTxt.Text = poTrans.getTownCity(.employed.sWrkAddrx, False, True, "")
                            Case 2
                                loTxt.Text = .employed.sWrkTelno
                            Case 3
                                If Not IsNumeric(.employed.nLenServc) Then .employed.nLenServc = 0
                                loTxt.Text = CInt(.employed.nLenServc)
                            Case 4
                                If Not IsNumeric(.employed.nSalaryxx) Then .employed.nSalaryxx = 0
                                loTxt.Text = FormatNumber(CDbl(.employed.nSalaryxx), 2)
                            Case 5
                                loTxt.Text = poTrans.getOccupation(.employed.sWrkTownx, False, True, "")
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
                        If .employed.cEmpStatx <> "" Then setStatEmployment(.employed.cEmpStatx, cmb09)
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
        Next
    End Sub

    Private Sub loadTransaction()
        Call loadMainInfo(Me.Panel1)
        Call loadApplicantInfo(Me.tabPages00)
        Call loadAppEmployment(Me.tabPages01)
        Call loadApResidence(Me.tabPages09)
        Call loadSpouseInfo(Me.tabPages03)
        Call loadSpouseEmployment(Me.tabPages04)
        Call loadSpouseRes(Me.tabPages10)
        Call loadOther(Me.tabPages06)
        Call loadComaker(Me.tabPages07)
        Call loadBanks()
        Call loadReference()
        Call loadChilderen()
        Call loadAppliMobile()
        Call loadAppliEmail()
        Call loadSpouseMobile()
        Call loadSpouseEmail()
        Call loadComakMobile()
    End Sub

    Private Sub initButton(ByVal fnValue As Integer)
        Dim lbShow As Boolean

        lbShow = IIf(fnValue = 0, True, False)

        Panel1.Enabled = lbShow
        cmdButton03.Visible = lbShow
        cmdButton08.Visible = lbShow
        cmdButton00.Visible = lbShow


        cmdButton04.Visible = Not lbShow
        cmdButton01.Visible = Not lbShow
        listItem.Visible = Not lbShow
        Panel1.Enabled = Not lbShow
    End Sub

    Private Sub txtAppli_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtAppli" And loTxt.ReadOnly = False Then
            With poTrans.Category.applicant_info
                Select Case loIndex
                    Case 0
                        .sFrstName = loTxt.Text
                    Case 1
                        .sLastName = loTxt.Text
                    Case 2
                        .sMiddName = loTxt.Text
                    Case 3
                        .sSuffixNm = loTxt.Text
                    Case 4
                        .sNickName = loTxt.Text
                    Case 5
                        .mobile_number(pnMobile).sMobileNo = loTxt.Text
                        Call loadAppliMobile()
                    Case 6
                        .email_address(pnEmail).sEmailAdd = loTxt.Text
                        Call loadAppliEmail()
                    Case 7
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dBirthDte = CDate(loTxt.Text)
                    Case 10
                        .sMaidenNm = loTxt.Text
                End Select
            End With
        End If
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            With poTrans.Category
                Select Case loIndex
                    Case 7
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .nAcctTerm = CInt(loTxt.Text)
                    Case 8
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .nDownPaym = CDbl(loTxt.Text)
                End Select
            End With
        End If
    End Sub

    Private Sub txtComak_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtComak" And loTxt.ReadOnly = False Then
            With poTrans.Category.comaker_info
                Select Case loIndex
                    Case 0
                        .sFrstName = loTxt.Text
                    Case 1
                        .sMiddName = loTxt.Text
                    Case 2
                        .sLastName = loTxt.Text
                    Case 5
                        .mobile_number(pnComakMobile).sMobileNo = loTxt.Text
                        Call loadComakMobile()
                    Case 7
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate(), xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dBirthDte = CDate(loTxt.Text)
                End Select
            End With
        End If
    End Sub

    Private Sub txtBanks_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtBanks" And loTxt.ReadOnly = False Then
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .disbursement_info.bank_account.sBankName = loTxt.Text
                End Select
            End With
        End If
    End Sub

    Private Sub loadBanks(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadBanks(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtBanks" Then
                        With poTrans.Category.disbursement_info.bank_account
                            Select Case loIndex
                                Case 0
                                    loTxt.Text = .sBankName
                            End Select
                            If (.sAcctType <> "") Then setBankType(.sAcctType, cmb26)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub txtRefer_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtRefer" And loTxt.ReadOnly = False Then
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .other_info.personal_reference(pnReference).sRefrNmex = loTxt.Text
                End Select
                Call loadReferenceCategory()
            End With
        End If
    End Sub

    Private Sub txtChild_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtChild" And loTxt.ReadOnly = False Then
            With poTrans.Category.disbursement_info.dependent_info
                Select Case loIndex
                    Case 0
                        .children(pnChild).sFullName = loTxt.Text
                    Case 1
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .children(pnChild).nDepdAgex = CInt(loTxt.Text)
                    Case 2
                        .children(pnChild).sSchlName = loTxt.Text
                End Select
                Call loadChildrenCategory()
            End With
        End If
    End Sub

    Private Sub txtSpous_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtSpous" And loTxt.ReadOnly = False Then
            With poTrans.Category.spouse_info.personal_info
                Select Case loIndex
                    Case 0
                        .sFrstName = loTxt.Text
                    Case 1
                        .sMiddName = loTxt.Text
                    Case 2
                        .sLastName = loTxt.Text
                    Case 3
                        .sSuffixNm = loTxt.Text
                    Case 4
                        .sNickName = loTxt.Text
                    Case 6
                        .mobile_number(pnSMobile).sMobileNo = loTxt.Text
                        Call loadSpouseMobile()
                    Case 7
                        .email_address(pnSEmail).sEmailAdd = loTxt.Text
                        Call loadSpouseEmail()
                    Case 8
                        If Not IsDate(loTxt.Text) Then
                            loTxt.Text = Format(p_oAppDriver.getSysDate, xsDATE_MEDIUM)
                        Else
                            loTxt.Text = Format(CDate(loTxt.Text), xsDATE_MEDIUM)
                        End If
                        .dBirthDte = CDate(loTxt.Text)
                End Select
            End With
        End If
    End Sub

    Private Sub txtOther_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtOther" And loTxt.ReadOnly = False Then
            With poTrans.Category
                Select Case loIndex
                    Case 1
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .disbursement_info.monthly_expenses.nElctrcBl = CDbl(loTxt.Text)
                    Case 2
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .disbursement_info.monthly_expenses.nWaterBil = CDbl(loTxt.Text)
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .disbursement_info.monthly_expenses.nLoanAmtx = CDbl(loTxt.Text)
                    Case 4
                        .disbursement_info.credit_card.sBankName = loTxt.Text
                    Case 5
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .disbursement_info.credit_card.nCrdLimit = CDbl(loTxt.Text)
                End Select
            End With
        End If
    End Sub

    Private Sub txtEmplo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtEmplo" And loTxt.ReadOnly = False Then
            With poTrans.Category.means_info
                Select Case loIndex
                    Case 0
                        .employed.sEmployer = loTxt.Text
                    Case 2
                        .employed.sWrkTelno = loTxt.Text
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .employed.nLenServc = CInt(loTxt.Text)
                    Case 4
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .employed.nSalaryxx = CDbl(loTxt.Text)
                    Case 6
                        .self_employed.sIndstBus = loTxt.Text
                    Case 8
                        .self_employed.sBusTownx = loTxt.Text
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .self_employed.nBusIncom = CDbl(loTxt.Text)
                    Case 10
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .self_employed.nBusLenxx = CInt(loTxt.Text)
                    Case 11
                        .other_income.sOthrIncm = loTxt.Text
                End Select
            End With
        End If
    End Sub

    Private Sub txtSEmpl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtSEmpl" And loTxt.ReadOnly = False Then
            With poTrans.Category.spouse_means
                Select Case loIndex
                    Case 0
                        .employed.sEmployer = loTxt.Text
                    Case 2
                        .employed.sWrkTelno = loTxt.Text
                    Case 3
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = 0
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .employed.nLenServc = CInt(loTxt.Text)
                    Case 4
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .employed.nSalaryxx = CDbl(loTxt.Text)
                    Case 6
                        .self_employed.sIndstBus = loTxt.Text
                    Case 9
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = FormatNumber(CDbl(0), 2)
                        Else
                            loTxt.Text = FormatNumber(CDbl(loTxt.Text), 2)
                        End If
                        .self_employed.nBusIncom = CDbl(loTxt.Text)
                    Case 10
                        If Not IsNumeric(loTxt.Text) Then
                            loTxt.Text = CInt(0)
                        Else
                            loTxt.Text = CInt(loTxt.Text)
                        End If
                        .self_employed.nBusLenxx = CInt(loTxt.Text)
                    Case 11
                        .other_income.sOthrIncm = loTxt.Text
                End Select
            End With
        End If
    End Sub

    Private Sub dgvDetail01_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail01.Click
        Dim pdRow As Integer
        pdRow = dgvDetail01.CurrentRow.Index
        ShowReference(pdRow)
    End Sub

    Private Sub dgvDetail02_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail02.Click
        Dim pdRow As Integer
        pdRow = dgvDetail02.CurrentRow.Index
        showChildren(pdRow)
    End Sub

    Private Sub loadSpouseRes(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadSpouseRes(loTxt)
            Else
                If (TypeOf loTxt Is Label) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "lblSpoRe" Then
                        With poTrans.Detail
                            Select Case loIndex
                                Case 20
                                    loTxt.Text = .sSpPresAd
                                Case 21
                                    loTxt.Text = .sSpPrevAd
                            End Select
                        End With
                    End If
                ElseIf (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpoRe" Then
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
                                    If (Not IsNothing(.rent_others)) Then
                                        loTxt.Text = .rent_others.nLenStayx
                                    End If
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
                                    loTxt.Text = poTrans.getTownCity(.permanent_address.sTownIDxx, False, True, "")
                                Case 14
                                    loTxt.Text = poTrans.getBarangay(.permanent_address.sBrgyIDxx, False, True, "")
                            End Select
                            If (.cOwnershp <> "") Then setOwnership(.cOwnershp, cmb10)
                            If (.cOwnOther <> "") Then setOwnedOther(.cOwnOther, cmb11)
                            If (.rent_others.cRntOther <> "") Then setRent(.rent_others.cRntOther, cmb12)
                            If (.cHouseTyp <> "") Then setHouseType(.cHouseTyp, cmb13)
                            If (.cGaragexx <> "") Then setGarage(.cGaragexx, cmb14)
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadApResidence(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadApResidence(loTxt)
            Else
                If (TypeOf loTxt Is Label) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "lblApRes" Then
                        With poTrans.Detail
                            Select Case loIndex
                                Case 20
                                    loTxt.Text = .sPresAddr
                                Case 21
                                    loTxt.Text = .sPrevAddr
                            End Select
                        End With
                    End If
                ElseIf (TypeOf loTxt Is TextBox) Then
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
                            If (.cOwnershp <> "") Then setOwnership(poTrans.Category.residence_info.cOwnershp, cmb04)
                            If (.cOwnOther <> "") Then setOwnedOther(poTrans.Category.residence_info.cOwnOther, cmb05)
                            If (.rent_others.cRntOther <> "") Then setRent(poTrans.Category.residence_info.rent_others.cRntOther, cmb06)
                            If (.cHouseTyp <> "") Then setHouseType(poTrans.Category.residence_info.cHouseTyp, cmb07)
                            If (.cGaragexx <> "") Then setGarage(.cGaragexx, cmb08)
                        End With
                    End If
                End If
            End If
        Next
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

    Private Sub txtApRes_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtApRes" And loTxt.ReadOnly = False Then
            With poTrans.Category.residence_info
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
                        If (Not IsNothing(.rent_others)) Then
                            If Not IsNumeric(loTxt.Text) Then
                                loTxt.Text = 0
                            Else
                                loTxt.Text = CInt(loTxt.Text)
                            End If
                            .rent_others.nRentExps = CDbl(loTxt.Text)
                        End If
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
            End With
        End If
    End Sub

    Private Sub txtResid_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtResid" And loTxt.ReadOnly = False Then
            With poTrans.Category.spouse_info.residence_info
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
                        If (Not IsNothing(.rent_others)) Then
                            If Not IsNumeric(loTxt.Text) Then
                                loTxt.Text = FormatNumber(0, 2)
                            Else
                                loTxt.Text = FormatNumber(loTxt.Text, 2)
                            End If
                            .rent_others.nRentExps = CDbl(loTxt.Text)
                        End If
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
            End With
        End If
    End Sub

    Private Sub chk00_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk00.CheckStateChanged
        With poTrans.Category.residence_info
            If chk00.Checked = True Then
                txtApRes09.Text = .present_address.sLandMark
                .permanent_address.sLandMark = .present_address.sLandMark
                txtApRes10.Text = .present_address.sHouseNox
                .permanent_address.sHouseNox = .present_address.sHouseNox
                txtApRes11.Text = .present_address.sAddress1
                .permanent_address.sAddress1 = .present_address.sAddress1
                txtApRes12.Text = .present_address.sAddress2
                .permanent_address.sAddress2 = .present_address.sAddress2
                txtApRes13.Text = poTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                .permanent_address.sTownIDxx = .present_address.sTownIDxx
                txtApRes14.Text = poTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                .permanent_address.sBrgyIDxx = .present_address.sBrgyIDxx
            Else
                txtApRes09.Text = ""
                .permanent_address.sLandMark = ""
                txtApRes10.Text = ""
                .permanent_address.sHouseNox = ""
                txtApRes11.Text = ""
                .permanent_address.sAddress1 = ""
                txtApRes12.Text = ""
                .permanent_address.sAddress2 = ""
                txtApRes13.Text = ""
                .permanent_address.sTownIDxx = ""
                txtApRes14.Text = ""
                .permanent_address.sBrgyIDxx = ""
            End If
        End With
    End Sub

    Private Sub chk01_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk01.CheckStateChanged
        With poTrans.Category.spouse_info.residence_info
            If chk01.Checked = True Then
                txtResid09.Text = .present_address.sLandMark
                .permanent_address.sLandMark = .present_address.sLandMark
                txtResid10.Text = .present_address.sHouseNox
                .permanent_address.sHouseNox = .present_address.sHouseNox
                txtResid11.Text = .present_address.sAddress1
                .permanent_address.sAddress1 = .present_address.sAddress1
                txtResid12.Text = .present_address.sAddress2
                .permanent_address.sAddress2 = .present_address.sAddress2
                txtResid13.Text = poTrans.getTownCity(.present_address.sTownIDxx, False, True, "")
                .permanent_address.sTownIDxx = .present_address.sTownIDxx
                txtResid14.Text = poTrans.getBarangay(.present_address.sBrgyIDxx, False, True, "")
                .permanent_address.sBrgyIDxx = .present_address.sBrgyIDxx
            Else
                txtResid09.Text = ""
                .permanent_address.sLandMark = ""
                txtResid10.Text = ""
                .permanent_address.sHouseNox = ""
                txtResid11.Text = ""
                .permanent_address.sAddress1 = ""
                txtResid12.Text = ""
                .permanent_address.sAddress2 = ""
                txtResid13.Text = ""
                .permanent_address.sTownIDxx = ""
                txtResid14.Text = ""
                .permanent_address.sBrgyIDxx = ""
            End If
        End With
    End Sub

    Private Sub dgvDetail03_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail03.Click
        Dim pdRow As Integer
        pdRow = dgvDetail03.CurrentRow.Index
        ShowMobile(pdRow)
    End Sub

    Private Sub dgvDetail04_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail04.Click
        Dim pnRow As Integer
        pnRow = dgvDetail04.CurrentRow.Index
        Call showEmail(pnRow)
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
            txtSpous07.Text = .email_address(fnRow).sEmailAdd
        End With
        pnSEmail = fnRow
    End Sub

    Private Sub chk02_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk02.CheckStateChanged
        With poTrans.Category.applicant_info
            If chk02.Checked = True Then
                .mobile_number(pnMobile).cPostPaid = "1"
            Else
                .mobile_number(pnMobile).cPostPaid = "0"
            End If
            Call loadAppliMobile()
        End With
    End Sub

    Private Sub dgvDetail05_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail05.Click
        Dim pnRow As Integer
        pnRow = dgvDetail05.CurrentRow.Index
        Call ShowSpouseMobile(pnRow)
    End Sub

    Private Sub dgvDetail06_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail06.Click
        Dim pnRow As Integer
        pnRow = dgvDetail06.CurrentRow.Index
        Call ShowSpouseEmail(pnRow)
    End Sub

    Private Sub dgvDetail07_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail07.Click
        Dim pnRow As Integer
        pnRow = dgvDetail07.CurrentRow.Index
        Call ShowComakMobile(pnRow)
    End Sub

    Private Sub Label56_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label56.Click

    End Sub

    Private Sub txtField07_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtField07.TextChanged

    End Sub

    Private Sub Label36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label36.Click

    End Sub
End Class
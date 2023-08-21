Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmCarApplicationEntryOld
    Dim lnMsg As String
    Dim poControl As Control
    Dim pnLoadx As Integer
    Dim pnRow As Integer = 0

    Private pxeModuleName As String = "E-commerce MC Credit Application"
    Private WithEvents poTrans As ggcGOCAS.MCApplication

    Private Sub frmMCCrecitApp_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMCCreditApp_Activated")

        If pnLoadx = 1 Then
            poTrans = New ggcGOCAS.MCApplication(p_oAppDriver)

            Call ClearFields()
            pnLoadx = 2
        End If
    End Sub

    Private Sub ClearFields()
        'initialize button depends on current mode
        initButton(0)

        'clear all text fields and lables
        Call ClearFields(Me.Panel1)

        'initialize grids
        Call initGridBank(dgvDetail00)
        Call initClientReference(dgvDetail01)
        Call initChildren(dgvDetail02)
        Call initNumber(dgvDetail03)

        'initialize listview
        Call initBank(listView00)
        Call initReference(listView01)
        Call initChildren(listView03)
        Call initMobile(ListView04)

        txtSeeks00.Focus()
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
        With poTrans.Category.personal_reference
            If .Count = 0 Then Exit Sub
            'txtRefer00.Text = poTrans.Category.personal_reference(fnRow).sRefrNmex
            'txtRefer01.Text = poTrans.Category.personal_reference(fnRow).sRefrAddx
        End With
        pnRow = fnRow
    End Sub

    Private Sub loadReferenceCategory()
        With dgvDetail01
            .Rows.Clear()
            If poTrans.Category.personal_reference.Count = 0 Then
                poTrans.Category.personal_reference.Add(New ggcGOCAS.MCApplication.personal_reference_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.personal_reference.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.personal_reference(lnCtr).sRefrNmex
                .Rows(lnCtr).Cells(2).Value = poTrans.Category.personal_reference(lnCtr).sRefrAddx
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail01.CurrentCell = dgvDetail01.Rows(dgvDetail01.RowCount - 1).Cells(0)
        dgvDetail01.Rows(dgvDetail01.RowCount - 1).Selected = True
    End Sub

    Private Sub loadMobile()
        Dim lsItems1 As ListViewItem
        Dim lsItems2 As ListViewItem
        Dim lsItems3 As ListViewItem

        Dim lsMobile1(3) As String
        Dim lsMobile2(3) As String
        Dim lsMobile3(3) As String

        lsMobile1(0) = "1"
        lsMobile1(1) = poTrans.Detail.sCPNumbr1
        lsMobile1(2) = poTrans.Detail.sCPTypex1
        lsItems1 = New ListViewItem(lsMobile1)

        lsMobile2(0) = "2"
        lsMobile2(1) = poTrans.Detail.sCPNumbr2
        lsMobile2(2) = poTrans.Detail.sCPTypex2
        lsItems2 = New ListViewItem(lsMobile2)

        lsMobile3(0) = "3"
        lsMobile3(1) = poTrans.Detail.sCPNumbr3
        lsMobile3(2) = poTrans.Detail.sCPTypex3
        lsItems3 = New ListViewItem(lsMobile3)

        With ListView04
            .Items.Clear()
            .Items.Add(lsItems1)
            .Items.Add(lsItems2)
            .Items.Add(lsItems3)
        End With
        Call loadMobileCategory()
    End Sub

    Private Sub loadMobileCategory()
        With dgvDetail03
            .Rows.Clear()
            If poTrans.Category.mobileno.Count = 0 Then
                poTrans.Category.mobileno.Add(New ggcGOCAS.MCApplication.mobileno_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.mobileno.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.mobileno(lnCtr).sCPNumber
                .Rows(lnCtr).Cells(2).Value = poTrans.Category.mobileno(lnCtr).sCPTypexx
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail03.CurrentCell = dgvDetail03.Rows(dgvDetail03.RowCount - 1).Cells(0)
        dgvDetail03.Rows(dgvDetail03.RowCount - 1).Selected = True
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
        lsChildren1(2) = poTrans.Detail.sChldAge1
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

        With poTrans.Category.children
            If .Count = 0 Then Exit Sub
            'txtChild00.Text = poTrans.Category.children(fnRow).sChldName
            'txtChild01.Text = poTrans.Category.children(fnRow).sChldAgex
            'txtChild02.Text = poTrans.Category.children(fnRow).sChldSchl
        End With
        pnRow = fnRow
    End Sub

    Private Sub loadChildrenCategory()
        With dgvDetail02
            .Rows.Clear()
            If poTrans.Category.children.Count = 0 Then
                poTrans.Category.children.Add(New ggcGOCAS.MCApplication.children_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.children.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.children(lnCtr).sChldName
                .Rows(lnCtr).Cells(2).Value = poTrans.Category.children(lnCtr).sChldAgex
                .Rows(lnCtr).Cells(3).Value = poTrans.Category.children(lnCtr).sChldSchl
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

        Call loadBanksCategory()
    End Sub

    Private Sub loadBanksCategory()
        With dgvDetail00
            .Rows.Clear()
            If poTrans.Category.bank_account.Count = 0 Then
                poTrans.Category.bank_account.Add(New ggcGOCAS.MCApplication.bank_account_param)
                .Rows.Add()
                Exit Sub
            End If

            Dim lnCtr As Integer = 0
            Do While lnCtr < poTrans.Category.bank_account.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(lnCtr + 1, "00")
                .Rows(lnCtr).Cells(1).Value = poTrans.Category.bank_account(lnCtr).sBankName
                .Rows(lnCtr).Cells(2).Value = poTrans.Category.bank_account(lnCtr).sBankBrch
                .Rows(lnCtr).Cells(3).Value = poTrans.Category.bank_account(lnCtr).sBankAcct
                lnCtr = lnCtr + 1
            Loop
        End With
        dgvDetail00.CurrentCell = dgvDetail00.Rows(dgvDetail00.RowCount - 1).Cells(0)
        dgvDetail00.Rows(dgvDetail00.RowCount - 1).Selected = True
    End Sub

    Private Sub ShowBanksInfo(ByVal fnRow)
        With poTrans.Category.bank_account
            If .Count = 0 Then Exit Sub
            'txtBanks00.Text = poTrans.Category.bank_account(fnRow).sBankName
            'txtBanks01.Text = poTrans.Category.bank_account(fnRow).sBankBrch
            'txtBanks02.Text = poTrans.Category.bank_account(fnRow).sBankAcct
        End With
        pnRow = fnRow
    End Sub

    Private Sub ShowMobile(ByVal fnRow)
        With poTrans.Category.mobileno
            If .Count = 0 Then Exit Sub
            'txtOther08.Text = poTrans.Category.mobileno(fnRow).sCPNumber
            'If poTrans.Category.mobileno(fnRow).sCPTypexx = "" Then Exit Sub
            'If poTrans.Category.mobileno(fnRow).sCPTypexx.ToLower = "prepaid" Then
            '    cmb00.SelectedIndex = 0
            'Else
            '    cmb00.SelectedIndex = 1
            'End If

        End With
        pnRow = fnRow
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0
                Me.Close()
            Case 1
                lnMsg = MsgBox("Cancel Edit? " & _
                               vbCrLf + "All Changes will not save??", vbYesNo + vbInformation, "Confirm")
                If lnMsg = vbYes Then
                    ClearFields()
                    initButton(0)
                End If
            Case 2
                Dim loFrm As frmAppApprovalPreview
                loFrm = New frmAppApprovalPreview
                loFrm.sTransNox = poTrans.Master("sTransNox")
                loFrm.sClientNme = poTrans.Master("sClientNm")
                loFrm.sQMatchNo = poTrans.Master("sQMatchNo")
                loFrm.txtField92.Text = ""
                loFrm.txtField92.Focus()
                loFrm.ShowDialog()
                If loFrm.Cancelled Then
                    MsgBox("Approval was cancelled by user!", vbCritical, "Notice")
                End If
            Case 3
                If poTrans.SearchTransaction("", True) = True Then
                    Call loadTransaction()
                    txtSeeks00.Text = poTrans.Master("sTransNox")
                    txtSeeks01.Text = poTrans.Master("sClientNm")
                Else
                    Call ClearFields()
                End If
            Case 8
                If poTrans.OpenTransaction(poTrans.Master("sTransNox")) Then
                    initButton(1)
                End If
            Case 10 ' add bank
            Case 11 'delete bank

            Case 12 'add reference
            Case 13 'delete Reference

            Case 14 'add children
            Case 15 'delete children

            Case 16 'add mobile
            Case 17 'delete mobile
        End Select
    End Sub

    Private Sub frmMCCrecitApp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Debug.Print("frmCreditApp_Load")
        If pnLoadx = 0 Then
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtField_KeyDown)

            Call grpEventHandler(Me, GetType(TextBox), "txtAppli", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtAppli", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtAppli", "Validating", AddressOf txtAppli_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtEmplo", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtEmplo", "Validating", AddressOf txtEmplo_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtSpous", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSpous", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSpous", "Validating", AddressOf txtSpous_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtSEmpl", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSEmpl", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtSEmpl", "Validating", AddressOf txtSEmpl_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtBanks", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtBanks", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtBanks", "Validating", AddressOf txtBanks_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtChild", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtChild", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtChild", "Validating", AddressOf txtChild_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtComak", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtComak", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtComak", "Validating", AddressOf txtComak_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtOther", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtOther", "Validating", AddressOf txtOther_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtRefer", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtRefer", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtRefer", "Validating", AddressOf txtRefer_Validating)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtSeeks
    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
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

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                If poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 0, True, False)) Then
                    Call loadTransaction()
                    txtSeeks00.Text = poTrans.Master("sTransNox")
                    txtSeeks01.Text = poTrans.Master("sClientNm")
                Else
                    Call ClearFields()
                End If
            ElseIf Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 5
                        loTxt.Text = poTrans.getBranch(loTxt.Text, True, False, poTrans.Category.sBranchCd)
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
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtSEmpl" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtChild" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtOther" Or _
                            LCase(Mid(loTxt.Name, 1, 8)) = "txtComak" Then
                            Select Case loIndex
                                Case Else
                                    loTxt.Text = ""
                            End Select
                        End If
                    End If
                End If
            End If
        Next
        pnRow = 0
        pdRow = 0

        setTransTat(-1)
        TabControl1.SelectedIndex = 0
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
                                loTxt.Text = Format(.Master("dTransact"), "MMMM dd, yyyy")
                            Case 2
                                loTxt.Text = .Master("sClientNm")
                            Case 81
                                loTxt.Text = .Detail.sPresAddr
                            Case 3
                                loTxt.Text = Format(.Detail.dBirthDte, xsDATE_MEDIUM)
                            Case 4
                                loTxt.Text = .Detail.nAgexxxxx
                            Case 5
                                loTxt.Text = poTrans.getBranch("", False, True, "")
                            Case 6
                                loTxt.Text = .Detail.MCModelx
                            Case 7
                                loTxt.Text = .Detail.nLoanTerm
                            Case 8
                                loTxt.Text = FormatNumber(poTrans.Category.nDownPaym, 2)
                        End Select
                        setTransTat(.Master("cTranStat"))
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
                                loTxt.Text = .sPresAddr
                            Case 8
                                loTxt.Text = .sPrevAddr
                            Case 9
                                loTxt.Text = .sLenStayx
                            Case 10
                                loTxt.Text = .sMobileNo
                            Case 11
                                loTxt.Text = .sEmailAdd
                            Case 12
                                loTxt.Text = .dBirthDte
                            Case 13
                                loTxt.Text = .sBrtPlace
                            Case 14
                                loTxt.Text = .nAgexxxxx
                            Case 15
                                loTxt.Text = .sMotherNm
                            Case 16
                                loTxt.Text = .sFatherNm
                            Case 17
                                loTxt.Text = .sParentAd
                                'Case 18
                                '    loTxt.Text = poTrans.getTownCity
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtAppli" Then
                    With poTrans.Category
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
                                loTxt.Text = .sPresAddr
                            Case 6
                                loTxt.Text = .sPrevAddr
                            Case 7
                                loTxt.Text = .sLenStayx
                            Case 8
                                loTxt.Text = .sMobileNo
                            Case 9
                                loTxt.Text = .sEmailAdd
                            Case 10
                                loTxt.Text = .dBirthDte
                            Case 11
                                loTxt.Text = .sBrtPlace
                            Case 12
                                loTxt.Text = .nAgexxxxx
                            Case 13
                                loTxt.Text = .sMotherNm
                            Case 14
                                loTxt.Text = .sFatherNm
                            Case 15
                                loTxt.Text = .sParentAd
                                'Case 16
                                '    loTxt.Text = .townID
                        End Select

                        'If .sGenderxx.ToLower = "male" Then
                        '    rbtn00.Select()
                        'ElseIf .sGenderxx.ToLower = "female" Then
                        '    rbtn01.Select()
                        'ElseIf .sGenderxx.ToLower = "lgbt" Then
                        '    rbtn02.Select()
                        'End If

                        If .sCvilStat.ToLower = "single" Then
                            cmb02.SelectedIndex = 0
                        ElseIf .sCvilStat.ToLower = "married" Then
                            cmb02.SelectedIndex = 1
                        ElseIf .sCvilStat.ToLower = "live-in" Then
                            cmb02.SelectedIndex = 2
                        ElseIf .sCvilStat.ToLower = "separated" Then
                            cmb02.SelectedIndex = 3
                        ElseIf .sCvilStat.ToLower = "widowed" Then
                            cmb02.SelectedIndex = 4
                        End If
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
                            Case 0
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
                            Case 10
                                loTxt.Text = .sBusiTele
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
                    With poTrans.Category
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sCompnyNm
                            Case 1
                                loTxt.Text = .sCompnyAd
                            Case 2
                                loTxt.Text = .sCompTele
                            Case 3
                                loTxt.Text = .sLenServe
                            Case 4
                                loTxt.Text = .sGrIncome
                            Case 5
                                loTxt.Text = .sEmplStat
                            Case 6
                                loTxt.Text = .sEmpPostn
                            Case 7
                                loTxt.Text = .sBusiness
                            Case 8
                                loTxt.Text = .sBusiAddr
                            Case 9
                                loTxt.Text = .sBusiTele
                            Case 10
                                loTxt.Text = .sBusIncom
                            Case 11
                                loTxt.Text = .sYrInBusi
                            Case 12
                                loTxt.Text = .sSourceIn
                        End Select

                        'If .sEmplType.ToLower = "government" Then
                        '    rbt03.Select()
                        'Else
                        '    rbt04.Select()
                        'End If
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
                                loTxt.Text = .sSpPresAd
                            Case 6
                                loTxt.Text = .sSpPrevAd
                            Case 7
                                loTxt.Text = .sSpLenSty
                            Case 8
                                loTxt.Text = .nSpAgexxx
                            Case 9
                                loTxt.Text = .sSpMobiNo
                            Case 10
                                loTxt.Text = .sSpEmailx
                            Case 11
                                loTxt.Text = .dSpBrtDte
                            Case 12
                                loTxt.Text = .sSpBrtPlc
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtSpous" Then
                    With poTrans.Category
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
                                loTxt.Text = .sSpPresAd
                            Case 6
                                loTxt.Text = .sSpPrevAd
                            Case 7
                                loTxt.Text = .sSpLenSty
                            Case 8
                                loTxt.Text = .nSpAgexxx
                            Case 9
                                loTxt.Text = .sSpMobiNo
                            Case 10
                                loTxt.Text = .sSpEmailx
                            Case 11
                                loTxt.Text = .dSpBrtDte
                            Case 12
                                loTxt.Text = .sSpBrtPlc
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
                                loTxt.Text = .sCLRelatn
                            Case 4
                                loTxt.Text = .sCLAddres
                            Case 5
                                loTxt.Text = .sCLEmploy
                            Case 6
                                loTxt.Text = .sCLContct
                            Case 7
                                loTxt.Text = .sCLEmailx
                            Case 8
                                loTxt.Text = .sCLBrtDte
                            Case 9
                                loTxt.Text = .sCLBrtPlc
                            Case 10
                                loTxt.Text = .sCOFrstNm
                            Case 11
                                loTxt.Text = .sCOMiddNm
                            Case 12
                                loTxt.Text = .sCORelatn
                            Case 13
                                loTxt.Text = .sCORelatn
                            Case 14
                                loTxt.Text = .sCOOccptn
                            Case 15
                                loTxt.Text = .sCONation
                            Case 16
                                loTxt.Text = .sCORemitt
                            Case 17
                                loTxt.Text = .sCOContct
                            Case 18
                                loTxt.Text = .sCORoamNo
                            Case 19
                                loTxt.Text = .sCOEmailx
                        End Select
                    End With
                End If
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtComak" Then
                    With poTrans.Category
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sCLFrstNm
                            Case 1
                                loTxt.Text = .sCLMiddNm
                            Case 2
                                loTxt.Text = .sCLLastNm
                            Case 3
                                loTxt.Text = .sCLRelatn
                            Case 4
                                loTxt.Text = .sCLAddres
                            Case 5
                                loTxt.Text = .sCLEmploy
                            Case 6
                                loTxt.Text = .sCLContct
                            Case 7
                                loTxt.Text = .sCLEmailx
                            Case 8
                                loTxt.Text = .sCLBrtDte
                            Case 9
                                loTxt.Text = .sCLBrtPlc
                            Case 10
                                loTxt.Text = .sCOFrstNm
                            Case 11
                                loTxt.Text = .sCOMiddNm
                            Case 12
                                loTxt.Text = .sCORelatn
                            Case 13
                                loTxt.Text = .sCORelatn
                            Case 14
                                loTxt.Text = .sCOOccptn
                            Case 15
                                loTxt.Text = .sCONation
                            Case 16
                                loTxt.Text = .sCORemitt
                            Case 17
                                loTxt.Text = .sCOContct
                            Case 18
                                loTxt.Text = .sCORoamNo
                            Case 19
                                loTxt.Text = .sCOEmailx
                        End Select
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
                                loTxt.Text = .sRentalxx
                            Case 1
                                loTxt.Text = .sElectric
                            Case 2
                                loTxt.Text = .sWaterBil
                            Case 3
                                loTxt.Text = .sOthrLoan
                            Case 4
                                loTxt.Text = .sCredtCrd
                            Case 5
                                loTxt.Text = .sCredtLmt
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
                    With poTrans.Category
                        Select Case loIndex
                            Case 0
                                loTxt.Text = .sRentalxx
                            Case 1
                                loTxt.Text = .sElectric
                            Case 2
                                loTxt.Text = .sWaterBil
                            Case 3
                                loTxt.Text = .sOthrLoan
                            Case 4
                                loTxt.Text = .sCredtCrd
                            Case 5
                                loTxt.Text = .sCredtLmt
                            Case 6
                                loTxt.Text = .sEducAttn
                            Case 7
                                loTxt.Text = .sLandmark
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
                    With poTrans.Category
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
            End If
        Next
    End Sub

    Private Sub loadTransaction()
        Call ClearFields()
        Call loadMainInfo(Me.Panel1)
        'Call loadApplicantInfo(Me.tabPages00)
        'Call loadAppEmployment(Me.tabPages01)
        'Call loadSpouseInfo(Me.tabPages03)
        'Call loadSpouseEmployment(Me.tabPages04)
        'Call loadOther(Me.tabPages06)
        'Call loadComaker(Me.tabPages07)
        Call loadBanks()
        Call loadReference()
        Call loadChilderen()
        Call loadMobile()
    End Sub

    Public Function setTransTat(ByVal nStat As Integer) As String
        Select Case nStat
            Case 0
                lblStatus.Text = "OPEN"
            Case 1
                lblStatus.Text = "CLOSED"
            Case 2
                lblStatus.Text = "POSTED"
            Case 3
                lblStatus.Text = "CANCELLED"
            Case 4
                lblStatus.Text = "VOID"
            Case Else
                lblStatus.Text = "UNKNOWN"
        End Select
    End Function

    Private Sub initButton(ByVal fnValue As Integer)
        Dim lbShow As Boolean

        lbShow = IIf(fnValue = 0, True, False)

        Panel1.Enabled = lbShow
        'cmdButton03.Visible = lbShow
        cmdButton08.Visible = lbShow
        cmdButton00.Visible = lbShow

        cmdButton04.Visible = Not lbShow
        'cmdButton02.Visible = Not lbShow
        'cmdButton01.Visible = Not lbShow
        Panel1.Enabled = Not lbShow
    End Sub

    Private Sub txtAppli_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtAppli" And loTxt.ReadOnly = False Then
            With poTrans.Category
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
                        .sPresAddr = loTxt.Text
                    Case 6
                        .sPrevAddr = loTxt.Text
                    Case 7
                        .sLenStayx = loTxt.Text
                    Case 8
                        .sMobileNo = loTxt.Text
                    Case 9
                        .sEmailAdd = loTxt.Text
                    Case 10
                        .dBirthDte = loTxt.Text
                    Case 11
                        .sBrtPlace = loTxt.Text
                    Case 12
                        .nAgexxxxx = loTxt.Text
                    Case 13
                        .sMotherNm = loTxt.Text
                    Case 14
                        .sFatherNm = loTxt.Text
                    Case 15
                        .sParentAd = loTxt.Text
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
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .sCLFrstNm = loTxt.Text
                    Case 1
                        .sCLMiddNm = loTxt.Text
                    Case 2
                        .sCLLastNm = loTxt.Text
                    Case 3
                        .sCLRelatn = loTxt.Text
                    Case 4
                        .sCLAddres = loTxt.Text
                    Case 5
                        .sCLEmploy = loTxt.Text
                    Case 6
                        .sCLContct = loTxt.Text
                    Case 7
                        .sCLEmailx = loTxt.Text
                    Case 8
                        .sCLBrtDte = loTxt.Text
                    Case 9
                        .sCLBrtPlc = loTxt.Text
                    Case 10
                        .sCOFrstNm = loTxt.Text
                    Case 11
                        .sCOMiddNm = loTxt.Text
                    Case 12
                        .sCORelatn = loTxt.Text
                    Case 13
                        .sCORelatn = loTxt.Text
                    Case 14
                        .sCOOccptn = loTxt.Text
                    Case 15
                        .sCONation = loTxt.Text
                    Case 16
                        .sCORemitt = loTxt.Text
                    Case 17
                        .sCOContct = loTxt.Text
                    Case 18
                        .sCORoamNo = loTxt.Text
                    Case 19
                        .sCOEmailx = loTxt.Text
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
                        .bank_account(pnRow).sBankName = loTxt.Text
                    Case 1
                        .bank_account(pnRow).sBankBrch = loTxt.Text
                    Case 2
                        .bank_account(pnRow).sBankAcct = loTxt.Text
                End Select
                Call loadBanksCategory()
            End With
        End If
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
                        .personal_reference(pnRow).sRefrNmex = loTxt.Text
                    Case 1
                        .personal_reference(pnRow).sRefrAddx = loTxt.Text
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
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .children(pnRow).sChldName = loTxt.Text
                    Case 1
                        .children(pnRow).sChldAgex = loTxt.Text
                    Case 2
                        .children(pnRow).sChldSchl = loTxt.Text
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
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .sSpFrstNm = loTxt.Text
                    Case 1
                        .sSpMiddNm = loTxt.Text
                    Case 2
                        .sSpLastNm = loTxt.Text
                    Case 3
                        .sSpSuffNm = loTxt.Text
                    Case 4
                        .sSpNickNm = loTxt.Text
                    Case 5
                        .sSpPresAd = loTxt.Text
                    Case 6
                        .sSpPrevAd = loTxt.Text
                    Case 7
                        .sSpLenSty = loTxt.Text
                    Case 8
                        .nSpAgexxx = loTxt.Text
                    Case 9
                        .sSpMobiNo = loTxt.Text
                    Case 10
                        .sSpEmailx = loTxt.Text
                    Case 11
                        .dSpBrtDte = loTxt.Text
                    Case 12
                        .sSpBrtPlc = loTxt.Text
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
                    Case 0
                        .sRentalxx = loTxt.Text
                    Case 1
                        .sElectric = loTxt.Text
                    Case 2
                        .sWaterBil = loTxt.Text
                    Case 3
                        .sOthrLoan = loTxt.Text
                    Case 4
                        .sCredtCrd = loTxt.Text
                    Case 5
                        .sCredtLmt = loTxt.Text
                    Case 6
                        .sEducAttn = loTxt.Text
                    Case 7
                        .sLandmark = loTxt.Text
                    Case 8
                        .mobileno(pnRow).sCPNumber = loTxt.Text
                        Call loadMobileCategory()
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
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .sCompnyNm = loTxt.Text
                    Case 1
                        .sCompnyAd = loTxt.Text
                    Case 2
                        .sCompTele = loTxt.Text
                    Case 3
                        .sLenServe = loTxt.Text
                    Case 4
                        .sGrIncome = loTxt.Text
                    Case 5
                        .sEmplStat = loTxt.Text
                    Case 6
                        .sEmpPostn = loTxt.Text
                    Case 7
                        .sBusiness = loTxt.Text
                    Case 8
                        .sBusiAddr = loTxt.Text
                    Case 9
                        .sBusiTele = loTxt.Text
                    Case 10
                        .sBusIncom = loTxt.Text
                    Case 11
                        .sYrInBusi = loTxt.Text
                    Case 12
                        .sSourceIn = loTxt.Text
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
            With poTrans.Category
                Select Case loIndex
                    Case 0
                        .sSpCompNm = loTxt.Text
                    Case 1
                        .sSpCompAd = loTxt.Text
                    Case 2
                        .sSpComTel = loTxt.Text
                    Case 3
                        .sSpLenSrv = loTxt.Text
                    Case 4
                        .sSpMonPay = loTxt.Text
                    Case 5
                        .sSpEmpSta = loTxt.Text
                    Case 6
                        .sSpEmpPos = loTxt.Text
                    Case 7
                        .sSpBusins = loTxt.Text
                    Case 8
                        .sSpBusiAd = loTxt.Text
                    Case 9
                        .sSpBusTel = loTxt.Text
                    Case 10
                        .sSpBusInc = loTxt.Text
                    Case 11
                        .sSpYrsBus = loTxt.Text
                    Case 12
                        .sSpSrcInc = loTxt.Text
                End Select
            End With
        End If
    End Sub

    Private Sub checkOtherInfo()
        'With poTrans.Category
        '    If rbtn00.Checked = True Then
        '        .sGenderxx = "0"
        '    ElseIf rbtn01.Checked = True Then
        '        .sGenderxx = "1"
        '    ElseIf rbtn02.Checked = True Then
        '        .sGenderxx = "2"
        '    End If
        '    .sCvilStat = CInt(cmb02.SelectedIndex)

        '    If rbt03.Checked = True Then
        '        .sEmplType = "0"
        '    ElseIf rbt04.Checked = True Then
        '        .sEmplType = "1"
        '    End If

        '    If .mobileno(pnRow).sCPTypexx.ToLower = "prepaid" Then
        '        .mobileno(pnRow).sCPTypexx = "0"
        '    Else
        '        .mobileno(pnRow).sCPTypexx = "1"
        '    End If
        'End With
    End Sub

    Private Sub dgvDetail00_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim pdRow As Integer
        'pdRow = dgvDetail00.CurrentRow.Index
        'ShowBanksInfo(pdRow)
    End Sub

    Private Sub dgvDetail01_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim pdRow As Integer
        'pdRow = dgvDetail01.CurrentRow.Index
        'ShowReference(pdRow)
    End Sub

    Private Sub dgvDetail02_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim pdRow As Integer
        'pdRow = dgvDetail02.CurrentRow.Index
        'showChildren(pdRow)
    End Sub

    Private Sub dgvDetail03_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim pdRow As Integer
        'pdRow = dgvDetail03.CurrentRow.Index
        'ShowMobile(pdRow)
    End Sub
End Class
Imports ggcAppDriver

'========================================================
' MC AR CONTRACT UI CONTROLLER
'========================================================
Public Class frmMCARContract

    '==================== PRIVATE VARIABLES ====================
    Private lnMsg As String
    Private poControl As Control
    Private pnLoadx As Integer


    Private psBranch As String = ""
    Private psClient As String = ""
    Private psGocas As String = ""

    Private psGocasTransNo As String = ""
    Private psTransStat As String = ""

    Private pxeModuleName As String = "MC AR Contract"

    Private p_ofrmMCARContractHist As frmMCARContractHist

    ' Transaction controller
    Private WithEvents poTrans As ggcLRTransaction.MCARContract

    '==================== FORM EVENTS ====================
    Private Sub frmMCARContract_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If pnLoadx = 1 Then
            poTrans = New ggcLRTransaction.MCARContract(p_oAppDriver, 0)

            initButton(poTrans.EditMode)
            initFields(poTrans.EditMode)
            ClearTextBoxes(Me)
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmMCARContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            'poTrans.resetEditmode()
            initButton(poTrans.EditMode.MODE_UNKNOWN)
            initFields(poTrans.EditMode.MODE_UNKNOWN)

            ' Register dynamic event handlers
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)

            pnLoadx = 1
        End If
    End Sub

    '==================== BUTTON HANDLER ====================
    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button = CType(sender, Button)
        Dim lnIndex As Integer = Val(Mid(loChk.Name, 10))
        Dim transaction As String = ""
        Select Case lnIndex

            Case 0, 9   ' Close
                Me.Close()

            Case 1  ' Retrieve contracts
                Dim msg As String = ""

                Dim dt As DataTable = poTrans.GetContractsByStatus(msg, psBranch, txtSeeks02.Text, txtSeeks03.Text)

                If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                    MessageBox.Show(msg, "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)

                    DataGridView1.DataSource = Nothing
                    initFields(poTrans.EditMode)
                    initButton(poTrans.EditMode)
                    txtSeeks01.Text = ""
                    txtSeeks02.Text = ""
                    txtSeeks03.Text = ""
                Else
                    InitializeContractsGrid(DataGridView1)
                    DataGridView1.DataSource = dt
                    initButton(poTrans.EditMode)
                    initFields(poTrans.EditMode)
                End If
            Case 2  ' browse
                If (poTrans.SearchTransaction(fsFilter, psBranch, txtSeeks02.Text, txtSeeks03.Text, True)) Then
                    initButton(poTrans.EditMode)
                    initFields(poTrans.EditMode)
                    LoadRecord(Me.Panel3)
                Else
                    ClearTextBoxes(Me)
                End If
            Case 3  ' Update
                If Not poTrans.UpdateTransaction(txtField00.Text) Then
                    initButton(poTrans.EditMode)
                End If

                initButton(poTrans.EditMode)
                initFields(poTrans.EditMode)
            Case 4 'save
                transaction = txtField00.Text
                If poTrans.SaveTransaction Then
                    If (p_oAppDriver.UserLevel >= xeUserRights.SUPERVISOR) Then
                        Dim result As DialogResult = MessageBox.Show(
                                        "Do you want to post this transaction?",
                                        "Information",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information)
                        If result = DialogResult.Yes Then
                            poTrans.ApproveTransaction(transaction, psGocasTransNo)
                        End If
                    End If
                    initButton(poTrans.EditMode)
                    initFields(poTrans.EditMode)
                    ClearTextBoxes(Me)
                End If
            Case 5 'cancel
                If Not String.IsNullOrEmpty(txtField00.Text) Then

                    If MessageBox.Show("Are you sure you want to cancel editing this transaction? 
                        Any unsaved changes will be lost.", "Information",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
                        ' Yes logic
                        poTrans.resetEditmode()
                        initButton(poTrans.EditMode)
                        initFields(poTrans.EditMode)
                        ClearTextBoxes(Me)
                    End If

                End If
            Case 6 'post
                transaction = txtField00.Text
                If poTrans.ApproveTransaction(transaction, psGocasTransNo) Then

                    initButton(poTrans.EditMode)
                    initFields(poTrans.EditMode)
                    ClearTextBoxes(Me)
                End If
            Case 7, 8
                ' Check if already open
                For Each f As Form In Me.MdiParent.MdiChildren
                    If TypeOf f Is frmMCARContractHist Then
                        f.BringToFront()
                        f.Activate()
                        Exit Sub
                    End If
                Next

                ' Not open → create new
                Dim frm As New frmMCARContractHist()

                frm.MdiParent = Me.MdiParent
                frm.Dock = DockStyle.None
                frm.StartPosition = FormStartPosition.CenterScreen

                frm.Show()
                frm.BringToFront()

            Case 10
                transaction = txtField00.Text
                If Not String.IsNullOrEmpty(txtField00.Text) Then
                    If poTrans.ExportTransaction(transaction, psGocasTransNo, psTransStat) Then
                        initButton(poTrans.EditMode)
                        initFields(poTrans.EditMode)
                        ClearTextBoxes(Me)
                    End If
                Else
                    MsgBox("No transaction is currently loaded. 
                            Please load a transaction to proceed.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "")
                End If

        End Select
    End Sub

    '==================== DATAGRID INITIALIZATION ====================
    Public Sub InitializeContractsGrid(ByRef dgv As DataGridView)

        dgv.AutoGenerateColumns = False
        dgv.Columns.Clear()

        dgv.ReadOnly = True
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToResizeRows = False
        dgv.AllowUserToResizeColumns = True
        dgv.RowHeadersVisible = False

        ' Counter column
        Dim colCounter As New DataGridViewTextBoxColumn()
        colCounter.Name = "RowNo"
        colCounter.HeaderText = "No"
        colCounter.ReadOnly = True
        dgv.Columns.Add(colCounter)

        ' Data columns
        dgv.Columns.Add("dapplied", "Date Applied")
        dgv.Columns.Add("sBranchCd", "Branch")
        dgv.Columns.Add("sClientNm", "Client Name")
        dgv.Columns.Add("sGOCASNox", "Reference No")

        Dim colContractNo As New DataGridViewTextBoxColumn()
        colContractNo.Name = "sContractNo"
        colContractNo.DataPropertyName = "sContractNo"
        colContractNo.Visible = False
        dgv.Columns.Add(colContractNo)

        dgv.Columns("dapplied").DataPropertyName = "dapplied"
        dgv.Columns("dapplied").DefaultCellStyle.Format = "yyyy-MM-dd"
        dgv.Columns("sBranchCd").DataPropertyName = "sBranchCd"
        dgv.Columns("sClientNm").DataPropertyName = "sClientNm"
        dgv.Columns("sGOCASNox").DataPropertyName = "sGOCASNox"

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        dgv.Columns("RowNo").FillWeight = 8
        dgv.Columns("dapplied").FillWeight = 25
        dgv.Columns("sBranchCd").FillWeight = 35
        dgv.Columns("sClientNm").FillWeight = 35
        dgv.Columns("sGOCASNox").FillWeight = 35

        AddHandler dgv.RowPostPaint, AddressOf dgv_RowPostPaint
    End Sub

    ' Draw row counter
    Private Sub dgv_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs)
        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim rowNumber As String = (e.RowIndex + 1).ToString()

        Dim headerBounds As Rectangle =
            New Rectangle(e.RowBounds.Left,
                          e.RowBounds.Top,
                          dgv.Columns(0).Width,
                          e.RowBounds.Height)

        TextRenderer.DrawText(e.Graphics,
                              rowNumber,
                              dgv.Font,
                              headerBounds,
                              dgv.ForeColor,
                              TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)
    End Sub

    '==================== GRID SELECTION ====================
    Private Sub dgvContracts_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles DataGridView1.CellDoubleClick

        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim gocasNo As String =
        If(IsDBNull(row.Cells("sContractNo").Value), "", row.Cells("sContractNo").Value.ToString())

        Dim proceed As Boolean = True ' default: proceed

        ' If currently updating, ask for confirmation
        If poTrans.EditMode = xeEditMode.MODE_UPDATE Then
            Dim result As MsgBoxResult
            result = MsgBox("You are currently updating a record. Loading another transaction will discard all unsaved data." & vbCrLf &
                        "Are you sure you want to proceed?",
                        MsgBoxStyle.YesNo + MsgBoxStyle.Critical,
                        "Confirm Action")

            If result = MsgBoxResult.No Then
                proceed = False ' User cancelled
            End If
        End If

        ' Only proceed if allowed
        If proceed Then
            poTrans.OpenTransaction(gocasNo)
            psGocasTransNo = row.Cells("sGOCASNox").Value
            LoadRecord(Me.Panel3)
            initButton(poTrans.EditMode)
        End If

    End Sub


    '==================== LOAD RECORD ====================
    Private Sub LoadRecord(ByVal loControl As Control)
        For Each loTxt As Control In loControl.Controls
            If loTxt.HasChildren Then
                LoadRecord(loTxt)

            ElseIf TypeOf loTxt Is TextBox Then
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
                    Dim loIndex As Integer = Val(Mid(loTxt.Name, 9))

                    With poTrans
                        Select Case loIndex
                            Case 0 : loTxt.Text = .Master("sTransNox")
                            Case 1
                                If Not IsDate(.Master("dTransact")) Then _
                                    .Master("dTransact") = p_oAppDriver.getSysDate
                                loTxt.Text = Format(.Master("dTransact"), xsDATE_MEDIUM)

                            Case 2 : loTxt.Text = .GetBranchName(.Master("sBranchCd"))

                            Case 3 : loTxt.Text = .Master("sReferNox")

                            Case 4 : loTxt.Text = .Master("sAcctNmbr")

                            Case 5 : loTxt.Text = .GetClientName(.Master("sClientID"))

                            Case 6 : loTxt.Text = .Master("sSerialID")

                            Case 7 : loTxt.Text = .GetSerialInfo(.Master("sSerialID"), .Master("sBranchCd")).Item1

                            Case 8 : loTxt.Text = .GetSerialInfo(.Master("sSerialID"), .Master("sBranchCd")).Item2

                            'Case 9 : loTxt.Text = If(IsDBNull(.Master("dFirstPay")), "", .Master("dFirstPay").ToString())

                            Case 10 : loTxt.Text = .Master("nAcctTerm")

                            Case 11 : loTxt.Text = Format(.Master("nDownPaym"), xsDECIMAL)

                            'Case 12 : loTxt.Text = Format(.Master("nPenaltyx"), xsDECIMAL)

                            Case 13 : loTxt.Text = Format(.Master("nMonAmort"), xsDECIMAL)

                            Case 14 : loTxt.Text = Format(.Master("nRebatesx"), xsDECIMAL)

                            Case 15 : loTxt.Text = Format(.Master("nPenaltyx"), xsDECIMAL)

                            Case 16 : loTxt.Text = If(IsDBNull(.Master("sRemarksx")), "", .Master("sRemarksx").ToString())
                        End Select

                        setTranStat(.Master("cTranStat"), lblStatus)
                        psTransStat = .Master("cTranStat")
                    End With
                End If
            End If
        Next
    End Sub

    '==================== UI STATE ====================
    Public Sub initButton(ByVal pnEditMode As Integer)
        ' All buttons
        Dim allButtons() As Button = {cmdButton00, cmdButton01, cmdButton02, cmdButton03, cmdButton04, cmdButton05, cmdButton06, cmdButton07, cmdButton08, cmdButton09, cmdButton10}

        ' Send all buttons to back first
        For Each btn In allButtons
            btn.SendToBack()
            btn.Visible = True ' Make sure visible if needed
        Next

        Select Case pnEditMode
            Case -1
                ' Desired visual order: top-most first
                cmdButton00.BringToFront()
                cmdButton01.BringToFront()
                cmdButton02.BringToFront()
                cmdButton07.BringToFront()
                cmdButton08.Visible = False
                cmdButton09.Visible = False
                cmdButton10.Visible = False

            Case 0
                cmdButton09.BringToFront()
                cmdButton08.BringToFront()
                cmdButton06.BringToFront()
                cmdButton03.BringToFront()
                cmdButton02.BringToFront()
                cmdButton01.BringToFront()

            Case 2
                cmdButton00.BringToFront()
                cmdButton05.BringToFront()
                cmdButton04.BringToFront()
                cmdButton01.BringToFront()
                cmdButton08.Visible = False
                cmdButton09.Visible = False
                cmdButton10.Visible = False
        End Select
    End Sub


    Public Sub initFields(ByVal pnEditMode As Integer)

        Dim enableFields As Boolean = Not (pnEditMode = poTrans.EditMode.MODE_READY Or pnEditMode = poTrans.EditMode.MODE_UNKNOWN)
        Dim alwaysDisabledFields As Integer() = {0, 6, 7, 8, 9, 16}

        For i As Integer = 0 To 16
            Dim txt = Me.Controls.Find("txtField" & i.ToString("00"), True) _
                                 .OfType(Of TextBox)() _
                                 .FirstOrDefault()

            If txt Is Nothing Then Continue For

            txt.ReadOnly = Not enableFields
            txt.TabStop = enableFields

            If alwaysDisabledFields.Contains(i) Then
                txt.ReadOnly = True
                txt.TabStop = False
                txt.BackColor = Color.Gainsboro
            Else
                txt.BackColor = Color.White
            End If
        Next
    End Sub

    '==================== TEXTBOX EVENTS ====================
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        loTxt.BackColor = If(loTxt.ReadOnly, SystemColors.Control, SystemColors.Window)
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        Dim loIndex As Integer = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" AndAlso Not loTxt.ReadOnly Then
            poTrans.Master(loIndex) = loTxt.Text

            Select Case loIndex
                Case 13
                    If Not IsNumeric(loTxt.Text) Then
                        MessageBox.Show("Invalid input", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                        HighlightInvalidInput(loTxt)
                        e.Cancel = True
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master("nMonAmort") = loTxt.Text
                    End If
                Case 14
                    If Not IsNumeric(loTxt.Text) Then
                        MessageBox.Show("Invalid input", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                        HighlightInvalidInput(loTxt)
                        e.Cancel = True
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master("nRebatesx") = loTxt.Text
                    End If
                Case 15
                    If Not IsNumeric(loTxt.Text) Then
                        MessageBox.Show("Invalid input", "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                        HighlightInvalidInput(loTxt)
                        e.Cancel = True
                    Else
                        loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                        poTrans.Master("nPenaltyx") = loTxt.Text
                    End If
            End Select
        End If
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox = CType(sender, TextBox)
        loTxt.BackColor = If(loTxt.ReadOnly, SystemColors.Control, SystemColors.Window)
    End Sub



    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down
                Select Case e.KeyCode
                    Case Keys.Down
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub frmLRPaymentEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control

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
                    Case 1
                        Call poTrans.searchBranch(loTxt.Text, False, True)
                        txtSeeks01.Text = poTrans.BranchName
                        If Not String.IsNullOrEmpty(txtField00.Text) Then
                            psBranch = poTrans.BranchCode
                        Else
                            psBranch = ""
                        End If


                End Select

            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub
    '==================== INPUT HIGHLIGHT ====================
    Public Sub HighlightInvalidInput(ByVal txt As TextBox, Optional ByVal duration As Integer = 1000)
        If txt Is Nothing Then Exit Sub

        Dim originalBack As Color = txt.BackColor
        Dim originalFore As Color = txt.ForeColor

        txt.BackColor = Color.Yellow
        txt.ForeColor = Color.Red
        txt.Focus()

        Dim t As New Timer()
        t.Interval = duration
        AddHandler t.Tick,
            Sub()
                txt.BackColor = originalBack
                txt.ForeColor = originalFore
                t.Stop()
                t.Dispose()
            End Sub
        t.Start()
    End Sub


    Public Sub ClearTextBoxes(ByVal parentCtrl As Control)

        For Each ctrl As Control In parentCtrl.Controls

            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Clear()
            End If

            ' Handle nested containers
            If ctrl.HasChildren Then
                ClearTextBoxes(ctrl)
            End If

        Next
        lblStatus.Text = "UNKNOWN"
    End Sub
End Class

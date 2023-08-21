Public Class frmAppApprovalPreview
    Dim p_nButton As Integer
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_sTransNox As String
    Private p_sClientNm As String
    Private p_sQMatchNo As String
    Private pxeModuleName As String = "frmApprovalPreview"

    Public WriteOnly Property sTransNox As String
        Set(ByVal value As String)
            p_sTransNox = value
        End Set
    End Property

    Public WriteOnly Property sClientNme As String
        Set(ByVal value As String)
            p_sClientNm = value
        End Set
    End Property

    Public WriteOnly Property sQMatchNo As String
        Set(ByVal value As String)
            p_sQMatchNo = value
        End Set
    End Property

    Public ReadOnly Property Cancelled() As Boolean
        Get
            Return p_nButton <> 1
        End Get
    End Property

    Private Sub frmAppApprovalPreview_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmAppApprovalPreview_Activated")
        'If poTrans.Master("cLoanType") = 'MC' then
        '    Me.Text = "MC Credit Application Approval"
        'Else
        '    Me.Text = "MP Credit Application Approval"
        'End If

        If pnLoadx = 1 Then
            txtField89.Text = p_sTransNox
            txtField90.Text = p_sClientNm
            txtField91.Text = p_sQMatchNo
            txtField92.Focus()
        End If
        pnLoadx = 2
    End Sub

    Private Sub frmAppApprovalPreview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Debug.Print("frmAppApprovalPreview_Load")
        If pnLoadx = 0 Then
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
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
            ElseIf TypeOf poControl Is Button Then
                loTxt = CType(poControl, System.Windows.Forms.Button)
            End If

            '###########################
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
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

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim loIndex As Integer
        loIndex = Val(Mid(loChk.Name, 10))

        Select Case loIndex
            Case 1
                If txtField92.Text <> "" Then
                    p_nButton = 1
                    MsgBox("Application Approved Successfully !!", vbInformation, pxeModuleName)
                    Me.Dispose()
                    'frmMCCreditApp.loadMaster()
                Else
                    MsgBox("Approval code must not be empty!!" + vbCrLf +
                           "Please check entry and try again!", vbCritical, pxeModuleName)
                    txtField92.Focus()
                End If
            Case 2
                p_nButton = 2
                Me.Dispose()
        End Select
    End Sub

    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))

        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            Select Case loIndex
                Case 92
            End Select
        End If
    End Sub
End Class
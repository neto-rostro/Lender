Imports ggcAppDriver
Public Class frmMCCreditAppResult
    Private pnLoadx As Integer
    Private p_sGOCasNo As String
    Private p_sCreditScore As String
    Private p_sWithCI As String
    Private p_sDownPayment As String
    Private p_sDownPaymentF As String
    Private p_sGoCasNoF As String
    Private p_sTransNox As String

    Public WriteOnly Property sTransNox As String
        Set(ByVal value As String)
            p_sTransNox = value
        End Set
    End Property


    Public WriteOnly Property GoCasNoF As String
        Set(ByVal value As String)
            p_sGoCasNoF = value
        End Set
    End Property

    Public WriteOnly Property GoCasNo As String
        Set(ByVal value As String)
            p_sGOCasNo = value
        End Set
    End Property

    Public WriteOnly Property CreditScore As String
        Set(ByVal value As String)
            p_sCreditScore = value
        End Set
    End Property

    Public WriteOnly Property WithCI As String
        Set(ByVal value As String)
            p_sWithCI = value
        End Set
    End Property

    Public WriteOnly Property DownPayment As String
        Set(ByVal value As String)
            p_sDownPayment = value
        End Set
    End Property

    Public WriteOnly Property DownPaymentF As String
        Set(ByVal value As String)
            p_sDownPaymentF = value
        End Set
    End Property

    Public Sub clearFields()
        lblField00.Text = ""
        lblField01.Text = ""
        lblField02.Text = ""
        lblField03.Text = ""
        lblField04.Text = ""
    End Sub

    Private Sub frmMCCreditAppresult_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMCCreditAppresult_Activated")
        If pnLoadx = 1 Then
            lblField00.Text = IIf(p_sGoCasNoF <> "", p_sGoCasNoF, p_sGOCasNo)
            lblField01.Text = IIf(p_sCreditScore = "", "", p_sCreditScore + " " + "Points")
            lblField02.Text = IIf(p_sWithCI = "1", "YES", "NO")
            lblField03.Text = IIf(p_sDownPaymentF <> 0, p_sDownPaymentF & " %", p_sDownPayment & " %")
            lblField04.Text = p_sTransNox
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmMCCreditAppresult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmMCCreditAppresult_Load")
        If pnLoadx = 0 Then
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Hide()
        End Select
    End Sub
End Class
Imports ggcAppDriver
Public Class frmAutoLedger
    Private pnLoadx As Integer
    Private p_sAcctNmbr As String
    Private p_sClientNm As String
    Private p_sAddressx As String
    Private p_sModelNme As String
    Private p_sPlateNop As String
    Private p_nInterest As Decimal
    Private p_nPrncipal As Decimal

    Public WriteOnly Property AccountNo As String
        Set(ByVal value As String)
            p_sAcctNmbr = value
        End Set
    End Property

    Public WriteOnly Property ClientName As String
        Set(ByVal value As String)
            p_sClientNm = value
        End Set
    End Property

    Public WriteOnly Property Address As String
        Set(ByVal value As String)
            p_sAddressx = value
        End Set
    End Property

    Public WriteOnly Property CarModel As String
        Set(ByVal value As String)
            p_sModelNme = value
        End Set
    End Property

    Public WriteOnly Property PlateNo As String
        Set(ByVal value As String)
            p_sPlateNop = value
        End Set
    End Property

    Public WriteOnly Property Interest As Decimal
        Set(ByVal value As Decimal)
            p_nInterest = value
        End Set
    End Property

    Public WriteOnly Property Principal As Decimal
        Set(ByVal value As Decimal)
            p_nPrncipal = value
        End Set
    End Property

    Private Sub frmLoanLedger_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLoanLedger_Activated")
        If pnLoadx = 1 Then
            txtField00.Text = p_sAcctNmbr
            txtField02.Text = p_sClientNm
            txtField03.Text = p_sAddressx
            txtField37.Text = p_sModelNme
            txtField42.Text = p_sPlateNop
            Call loadLedger()
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmLoanLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLoanLedger_Load")
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
                Me.Dispose()
        End Select
    End Sub
    Private Sub loadLedger()
        Dim lsSQL As String
        Dim loDta As DataTable
        Dim lnCtr As Integer
        Dim lnABalance As Decimal

        lsSQL = "SELECT" & _
                      "  a.dTransact" & _
                      ", a.cOffPaymx" & _
                      ", a.sReferNox" & _
                      ", a.nPaidAmtx" & _
                      ", a.nIntAmtxx" & _
                      ", a.nRebatesx" & _
                      ", a.nPenaltyx" & _
                      ", a.nABalance" & _
                      ", a.nCredtAmt" & _
                      ", a.nDebitAmt" & _
                      ", a.sRemarksx" & _
                      ", a.nEntryNox" & _
         " FROM LR_Ledger a" & _
            " LEFT JOIN Client_Master b ON a.sCollIDxx = b.sClientID" & _
            " LEFT JOIN Branch c ON a.sBranchCD = c.sBranchCD" & _
         " WHERE a.sAcctNmbr = " & strParm(p_sAcctNmbr) & _
         " ORDER BY nEntryNox"
        loDta = p_oAppDriver.ExecuteQuery(lsSQL)

        With dgView
            .Rows.Clear()

            If loDta.Rows.Count = 0 Then
                loDta.Rows.Add()
                Exit Sub
            End If

            lnCtr = 0
            lnABalance = (p_nPrncipal + p_nInterest)
            Do While lnCtr < loDta.Rows.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(loDta(lnCtr).Item("dTransact"), xsDATE_SHORT)
                .Rows(lnCtr).Cells(1).Value = IIf(loDta(lnCtr).Item("cOffPaymx") = "0", "F", "O")
                .Rows(lnCtr).Cells(2).Value = loDta(lnCtr).Item("sReferNox")
                .Rows(lnCtr).Cells(3).Value = Format(loDta(lnCtr).Item("nPaidAmtx"), xsDECIMAL)
                .Rows(lnCtr).Cells(4).Value = Format(loDta(lnCtr).Item("nIntAmtxx"), xsDECIMAL)
                .Rows(lnCtr).Cells(5).Value = Format(IFNull(loDta(lnCtr).Item("nRebatesx"), 0.0), xsDECIMAL)
                .Rows(lnCtr).Cells(6).Value = Format(loDta(lnCtr).Item("nPenaltyx"), xsDECIMAL)
                lnABalance = lnABalance - (loDta(lnCtr).Item("nPaidAmtx") + loDta(lnCtr).Item("nIntAmtxx")) 'jovan
                .Rows(lnCtr).Cells(7).Value = Format(lnABalance, xsDECIMAL)
                .Rows(lnCtr).Cells(8).Value = Format(loDta(lnCtr).Item("nCredtAmt"), xsDECIMAL)
                .Rows(lnCtr).Cells(9).Value = Format(loDta(lnCtr).Item("nDebitAmt"), xsDECIMAL)
                .Rows(lnCtr).Cells(10).Value = loDta(lnCtr).Item("sRemarksx")
                lnCtr = lnCtr + 1
            Loop
        End With
    End Sub

End Class
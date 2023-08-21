Imports ggcAppDriver

Public Class frmLRLedger
    Private pnLoadx As Integer
    Private p_sAcctNmbr As String
    Private p_sClientNm As String
    Private p_sAddressx As String
    Private p_sCompnyNm As String

    Public WriteOnly Property AccountNo As String
        Set(value As String)
            p_sAcctNmbr = value
        End Set
    End Property

    Public WriteOnly Property ClientName As String
        Set(value As String)
            p_sClientNm = value
        End Set
    End Property

    Public WriteOnly Property Address As String
        Set(value As String)
            p_sAddressx = value
        End Set
    End Property

    Public WriteOnly Property Company As String
        Set(value As String)
            p_sCompnyNm = value
        End Set
    End Property

    Private Sub frmLRLedger_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLRLedger_Activated")
        If pnLoadx = 1 Then
            txtField00.Text = p_sAcctNmbr
            txtField80.Text = p_sClientNm
            txtField81.Text = p_sAddressx
            txtField85.Text = p_sCompnyNm

            Call loadLedger()

            pnLoadx = 2
        End If
    End Sub

    Private Sub frmLRLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLRLedger_Load")
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

        'mac 2020-09-01
        '   get the interest to pay
        lsSQL = "SELECT nInterest FROM LR_Master WHERE sAcctNmbr = " & strParm(p_sAcctNmbr)

        loDta = p_oAppDriver.ExecuteQuery(lsSQL)

        If loDta.Rows.Count = 0 Then Exit Sub

        Dim lnInterest As Double = loDta(0)("nInterest")

        lsSQL = "SELECT" & _
                      "  a.dTransact" & _
                      ", a.cOffPaymx" & _
                      ", a.cTrantype" & _
                      ", a.sReferNox" & _
                      ", a.nPaidAmtx" & _
                      ", a.nIntAmtxx" & _
                      ", a.nPenaltyx" & _
                      ", a.nABalance" & _
                      ", a.nCredtAmt" & _
                      ", a.nDebitAmt" & _
                      ", a.nMonDelay" & _
                      ", IFNULL(b.sCompnyNm, c.sBranchNm) sCollName" & _
                      ", a.sRemarksx" & _
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
            Do While lnCtr < loDta.Rows.Count
                .Rows.Add()
                .Rows(lnCtr).Cells(0).Value = Format(loDta(lnCtr).Item("dTransact"), xsDATE_SHORT)
                .Rows(lnCtr).Cells(1).Value = IIf(loDta(lnCtr).Item("cOffPaymx") = "0", "F", "O")
                .Rows(lnCtr).Cells(2).Value = getTranType(loDta(lnCtr).Item("cTrantype"))
                .Rows(lnCtr).Cells(3).Value = loDta(lnCtr).Item("sReferNox")
                .Rows(lnCtr).Cells(4).Value = loDta(lnCtr).Item("nPaidAmtx")
                .Rows(lnCtr).Cells(5).Value = loDta(lnCtr).Item("nIntAmtxx")
                .Rows(lnCtr).Cells(6).Value = loDta(lnCtr).Item("nPenaltyx")

                'mac 2020.09.01
                '   add interest balance to account balance
                lnInterest -= loDta(lnCtr).Item("nIntAmtxx")
                .Rows(lnCtr).Cells(7).Value = loDta(lnCtr).Item("nABalance") + lnInterest

                'original code
                '   .Rows(lnCtr).Cells(7).Value = loDta(lnCtr).Item("nABalance")

                .Rows(lnCtr).Cells(8).Value = loDta(lnCtr).Item("nCredtAmt")
                .Rows(lnCtr).Cells(9).Value = loDta(lnCtr).Item("nDebitAmt")
                .Rows(lnCtr).Cells(10).Value = loDta(lnCtr).Item("nMonDelay")
                .Rows(lnCtr).Cells(11).Value = loDta(lnCtr).Item("sCollName")
                .Rows(lnCtr).Cells(12).Value = loDta(lnCtr).Item("sRemarksx")
                lnCtr = lnCtr + 1
            Loop
        End With
    End Sub

    Private Function getTranType(ByVal fcTranType As String) As String
        Select Case fcTranType
            Case "0"
                Return "MP"
            Case "1"
                Return "Dm"
            Case "2"
                Return "Cm"
            Case "3"
                Return "PN"
            Case Else
                Return "UN"
        End Select
    End Function
End Class
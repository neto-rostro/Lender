Imports ggcAppDriver

Public Class frmCarRegistrationLedger
    Private pnLoadx As Integer
    Private p_serialId As String
    Private p_sclientNm As String
    Private p_plateNo As String


    Public WriteOnly Property SerialID As String
        Set(ByVal value As String)
            p_serialId = value
        End Set
    End Property

    Public WriteOnly Property ClientName As String
        Set(ByVal value As String)
            p_sclientNm = value
        End Set
    End Property

    Public WriteOnly Property PlateNo As String
        Set(ByVal value As String)
            p_plateNo = value
        End Set
    End Property

    Private Sub frmLRLedger_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLRLedger_Activated")
        If pnLoadx = 1 Then
            txtField00.Text = p_serialId
            txtField01.Text = p_sclientNm
            txtField02.Text = p_plateNo

            'Call loadLedger()

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
         " WHERE a.sAcctNmbr = " & strParm(p_serialId) & _
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
                .Rows(lnCtr).Cells(0).Value = Format(loDta(lnCtr).Item("dRegister"), xsDATE_SHORT)
                .Rows(lnCtr).Cells(3).Value = loDta(lnCtr).Item("sFileNoxx")
                .Rows(lnCtr).Cells(3).Value = loDta(lnCtr).Item("sCRNoxxxx")
                .Rows(lnCtr).Cells(3).Value = loDta(lnCtr).Item("sRegORNox")
                .Rows(lnCtr).Cells(4).Value = loDta(lnCtr).Item("sStickrNo")
                lnCtr = lnCtr + 1
            Loop
        End With
    End Sub

End Class
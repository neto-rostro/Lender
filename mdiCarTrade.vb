Imports ggcAppDriver
Imports System.Globalization

Public Class mdiCarTrade
    Private p_frmLoanView As frmAutoARReg
    Private p_frmCarBrand As frmCarBrand
    Private p_frmCarColor As frmCarColor
    Private p_frmCarModel As frmCarModel
    Private p_frmSysUser As frmSysUser
    Private p_frmCarSerialRegistration As frmCarSerialRegistration
    Private p_frmCTLoanManagement As frmCTLoanManagement
    Private p_frmLoanPosting As frmLoanPosting
    Private p_frmCTBillingMaster As frmCTBillingMaster
    Private p_frmCTPosting As frmCTPosting
    Private p_frmPayment As frmPayment
    Private p_frmReceived As frmReceived
    Private p_frmLenderReg As frmLenderReg
    Private p_frmBillingReg As frmBillingReg


    Private Sub CollateralToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollateralToolStripMenuItem.Click
        If p_frmCarBrand Is Nothing Then
            p_frmCarBrand = New frmCarBrand

        End If
        showModalForm(p_frmCarBrand, Me)
        p_frmCarBrand = Nothing
    End Sub

    Private Sub CompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompanyToolStripMenuItem.Click
        If p_frmCarModel Is Nothing Then
            p_frmCarModel = New frmCarModel
        End If
        showModalForm(p_frmCarModel, Me)
        p_frmCarModel = Nothing
    End Sub

    Private Sub ColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ColorToolStripMenuItem.Click
        If p_frmCarColor Is Nothing Then
            p_frmCarColor = New frmCarColor
        End If
        showModalForm(p_frmCarColor, Me)
        p_frmCarColor = Nothing
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If MsgBox("Are you sure to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Car Trade Confirmation") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub AdjustmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'showModalForm(frmCarSerialRegistration, Me)
    End Sub

    Private Sub LRAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRAccountToolStripMenuItem.Click
        'showModalForm(frmCarSerial, Me)
    End Sub

    Private Sub ForPostingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForPostingToolStripMenuItem.Click
        If p_frmLoanPosting Is Nothing Then
            p_frmLoanPosting = New frmLoanPosting
            p_frmLoanPosting.AccountStatus = "-1"
        End If
        showModalForm(p_frmLoanPosting, Me)
        p_frmLoanPosting = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem.Click
        If p_frmCTLoanManagement Is Nothing Then
            p_frmCTLoanManagement = New frmCTLoanManagement
            p_frmCTLoanManagement.AccountStatus = "-1"
        End If
        showModalForm(p_frmCTLoanManagement, Me)
        p_frmCTLoanManagement = Nothing
    End Sub

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserToolStripMenuItem.Click
        showModalForm(frmSysUser, Me)
    End Sub

    Private Sub EntryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem1.Click
        If p_frmCTBillingMaster Is Nothing Then
            p_frmCTBillingMaster = New frmCTBillingMaster
            p_frmCTBillingMaster.TranStatus = "0"
        End If
        showModalForm(p_frmCTBillingMaster, Me)
        p_frmCTBillingMaster = Nothing
    End Sub

    Private Sub ConfirmationPostingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmationPostingToolStripMenuItem.Click
        If p_frmCTPosting Is Nothing Then
            p_frmCTPosting = New frmCTPosting
            p_frmCTPosting.TranStatus = "0"
        End If
        showModalForm(p_frmCTPosting, Me)
        p_frmCTPosting = Nothing
    End Sub

    Private Sub PaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentToolStripMenuItem.Click
        If p_frmPayment Is Nothing Then
            p_frmPayment = New frmPayment
            p_frmPayment.TranStatus = "10"
        End If
        showModalForm(p_frmPayment, Me)
        p_frmPayment = Nothing
    End Sub

    Private Sub ReceiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiveToolStripMenuItem.Click
        If p_frmReceived Is Nothing Then
            p_frmReceived = New frmReceived
            p_frmReceived.TranStatus = "1"
        End If
        showModalForm(p_frmReceived, Me)
        p_frmReceived = Nothing
    End Sub

    Private Sub LenderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LenderToolStripMenuItem.Click
        If p_frmLenderReg Is Nothing Then
            p_frmLenderReg = New frmLenderReg
            p_frmLenderReg.AccountStatus = "-1"
        End If
        showModalForm(p_frmLenderReg, Me)
        p_frmLenderReg = Nothing
    End Sub

    Private Sub BillingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillingToolStripMenuItem.Click
        If p_frmBillingReg Is Nothing Then
            p_frmBillingReg = New frmBillingReg
            p_frmBillingReg.TranStatus = "10234"
        End If
        showModalForm(p_frmBillingReg, Me)
        p_frmBillingReg = Nothing
    End Sub

    Private Sub PaymentAtBranchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentAtBranchToolStripMenuItem.Click
        Dim loRpt As clsBranchReport
        loRpt = New clsBranchReport(p_oAppDriver)

        If loRpt.getParameter() Then
            Call loRpt.ReportTrans()
        End If
    End Sub

    Private Sub CustomerInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomerInfoToolStripMenuItem.Click
        Dim loRpt As clsBillReport
        loRpt = New clsBillReport(p_oAppDriver)

        If loRpt.getParameter() Then
            Call loRpt.ReportTrans()
        End If
    End Sub

    Private Sub ActiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActiveToolStripMenuItem.Click
        If p_frmLoanView Is Nothing Then
            p_frmLoanView = New frmAutoARReg
            p_frmLoanView.AccountStatus = "-1"
        End If
        showModalForm(p_frmLoanView, Me)
        p_frmLoanView = Nothing
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        If p_frmLoanView Is Nothing Then
            p_frmLoanView = New frmAutoARReg
            p_frmLoanView.AccountStatus = "1"
        End If
        showModalForm(p_frmLoanView, Me)
        p_frmLoanView = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem2.Click
        showModalForm(frmCarApplicationEntry, Me)
    End Sub

    Private Sub ApprovalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovalToolStripMenuItem.Click
        showModalForm(frmCarApplicationApproval, Me)
    End Sub

    Private Sub CarApplicationHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarApplicationHistoryToolStripMenuItem.Click
        showModalForm(frmCarApplicationHistory, Me)
    End Sub
End Class

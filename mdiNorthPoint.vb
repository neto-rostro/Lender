Imports ggcAppDriver
Imports System.Globalization
Imports ggcClient

Public Class mdiNorthPoint
    Private p_frmLoanManagement As frmLoanManagement
    Private p_frmLoanPosting As frmLoanPosting
    Private p_frmCarSerial As frmCarSerial
    Private p_frmCarBrand As frmCarBrand
    Private p_frmCarColor As frmCarColor
    Private p_frmCarModel As frmCarModel
    Private p_frmSysUser As frmSysUser
    Private p_frmCTBillingMaster As frmCTBillingMaster
    Private p_frmCTPosting As frmCTPosting
    Private p_frmPayment As frmPayment
    Private p_frmReceived As frmReceived
    Private p_frmBillingReg As frmBillingReg
    Private p_frmLenderReg As frmLenderReg
    Private p_frmLoanView As frmAutoARReg

    Private Sub LoanManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoanManagementToolStripMenuItem.Click
        'showModalForm(frmCarSerial, Me)
    End Sub

    Private Sub CarTradeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarTradeToolStripMenuItem.Click

    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        If MsgBox("Are you sure to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "NorthPoint Confirmation") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub LenderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LenderToolStripMenuItem.Click

    End Sub

    Private Sub CarColToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarColToolStripMenuItem.Click
        If p_frmCarColor Is Nothing Then
            p_frmCarColor = New frmCarColor

        End If
        showModalForm(p_frmCarColor, Me)
        p_frmCarBrand = Nothing
    End Sub

    Private Sub CarBrandToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarBrandToolStripMenuItem.Click
        If p_frmCarBrand Is Nothing Then
            p_frmCarBrand = New frmCarBrand
        End If
        showModalForm(p_frmCarBrand, Me)
        p_frmCarBrand = Nothing
    End Sub

    Private Sub CarModelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarModelToolStripMenuItem.Click
        'showModalForm(frmCarModel, Me)
        If p_frmCarModel Is Nothing Then
            p_frmCarModel = New frmCarModel

        End If
        showModalForm(p_frmCarModel, Me)
        p_frmCarModel = Nothing
    End Sub

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserToolStripMenuItem.Click
        showModalForm(frmSysUser, Me)
    End Sub

    Private Sub EntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem.Click
        If p_frmLoanManagement Is Nothing Then
            p_frmLoanManagement = New frmLoanManagement
            p_frmLoanManagement.AccountStatus = "-1"
            'p_frmLoanManagement.AccountStatus = "10234"
        End If
        showModalForm(p_frmLoanManagement, Me)
        p_frmLoanManagement = Nothing
    End Sub

    Private Sub PostingConfirmationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem.Click
        If p_frmLoanPosting Is Nothing Then
            p_frmLoanPosting = New frmLoanPosting
            p_frmLoanPosting.AccountStatus = "-1"
            'p_frmLoanPosting.AccountStatus = "10234"
        End If
        showModalForm(p_frmLoanPosting, Me)
        p_frmLoanPosting = Nothing
    End Sub

    Private Sub PostingConfirmationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem1.Click
        If p_frmCTPosting Is Nothing Then
            p_frmCTPosting = New frmCTPosting
            p_frmCTPosting.TranStatus = "0"
        End If
        showModalForm(p_frmCTPosting, Me)
        p_frmCTPosting = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem1.Click
        If p_frmCTBillingMaster Is Nothing Then
            p_frmCTBillingMaster = New frmCTBillingMaster
            p_frmCTBillingMaster.TranStatus = "0"
        End If
        showModalForm(p_frmCTBillingMaster, Me)
        p_frmCTBillingMaster = Nothing
    End Sub

    Private Sub PaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentToolStripMenuItem.Click
        If p_frmPayment Is Nothing Then
            p_frmPayment = New frmPayment
            p_frmPayment.TranStatus = "10"
        End If
        showModalForm(p_frmPayment, Me)
        p_frmPayment = Nothing
    End Sub

    Private Sub ReceivedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivedToolStripMenuItem.Click
        If p_frmReceived Is Nothing Then
            p_frmReceived = New frmReceived
            p_frmReceived.TranStatus = "1"
        End If
        showModalForm(p_frmReceived, Me)
        p_frmReceived = Nothing
    End Sub

    Private Sub LenderToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LenderToolStripMenuItem2.Click
        If p_frmLenderReg Is Nothing Then
            p_frmLenderReg = New frmLenderReg
            p_frmLenderReg.AccountStatus = "-1"
        End If
        showModalForm(p_frmLenderReg, Me)
        p_frmLenderReg = Nothing
    End Sub

    Private Sub BillingToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillingToolStripMenuItem1.Click
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

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If MsgBox("Are you sure to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "NorthPoint Confirmation") = MsgBoxResult.Yes Then
            Me.Close()
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

    Private Sub ClosedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosedToolStripMenuItem.Click
        If p_frmLoanView Is Nothing Then
            p_frmLoanView = New frmAutoARReg
            p_frmLoanView.AccountStatus = "1"
        End If
        showModalForm(p_frmLoanView, Me)
        p_frmLoanView = Nothing
    End Sub

End Class
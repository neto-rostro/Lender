Imports ggcAppDriver
Imports System.Globalization
Imports System.Threading

Public Class mdiMain
    Private Const pxeMONITOR_INTERVAL As Integer = 15 'sys monitor running interval in seconds
    Private p_nCtr As Integer 'seconds counter of Timer1

    Private p_bLoaded As Boolean
    Private p_isEvaluator As String
    Private p_ofrmCollection As frmMCCreditAppReview
    Private p_ofrmCreditOverride As frmMCCreditAppOverride
    Private p_ofrmEvaluator As frmMCCreditAppCategorization
    Private p_frmLoanView As frmAutoARReg
    Private p_ofrmLRActive As frmLRMasterReg
    Private p_ofrmLRClosed As frmLRMasterReg
    Private p_ofrmMCActive As frmMCMasterReg
    Private p_ofrmMCClosed As frmMCMasterReg
    Private p_ofrmMPActive As frmMPMasterReg
    Private p_ofrmMPCLosed As frmMPMasterReg
    Private p_nLocation As Point
    Private p_nIdleTIme As Date
    Private p_bRunning As Boolean

    Private p_ofrmARMPEntry As frmARPaymentEntry
    Private p_ofrmARCBEntry As frmARPaymentEntry
    Private p_ofrmARDBEntry As frmARPaymentEntry
    Private p_ofrmLREntry As frmLRPaymentEntry
    Private p_ofrmMPPaymentEntry As frmMPPaymentEntry
    Private p_ofrmCarPaymentEntry As frmCarPaymentEntry

    Private p_ofrmLRPosting As frmLRPaymentPosting
    Private p_ofrmCarPaymentPosting As frmCarPaymentPosting
    Private p_ofrmCarPostingPR As frmCarPaymentPostingPr
    Private p_ofrmLRPaymentPostingPR As frmLRPaymentPostingPR
    Private p_ofrmMPPaymentPosting As frmMPPaymentPosting
    Private p_ofrmMPPPaymentPostingPR As frmMPPaymentPRPosting

    Private p_ofrmARMPPost As frmARPaymentApproval
    Private p_ofrmARCBPost As frmARPaymentApproval
    Private p_ofrmARDBPost As frmARPaymentApproval

    Private p_ofrmARMPReg As frmARPaymentReg
    Private p_ofrmARCBReg As frmARPaymentReg
    Private p_ofrmARDBReg As frmARPaymentReg
    Private p_ofrmCarPaymentReg As frmCarPaymentReg
    Private p_ofrmLrPaymentReg As frmLRPaymentReg
    Private p_ofrmMPPaymentEntryReg As frmMPPaymentEntryReg

    Private p_ofrmARMPEntryPR As frmARPaymentEntryPR
    Private p_ofrmARCBEntryPR As frmARPaymentEntryPR
    Private p_ofrmARDBEntryPR As frmARPaymentEntryPR
    Private p_ofrmMPPaymentEntryPR As frmMPPaymentEntryPR
    Private p_ofrmCarPaymentPR As frmCarPaymentPR
    Private p_ofrmLRPaymentEntryPR As frmLRPaymentEntryPR

    Private p_ofrmARMPPostPR As frmARPaymentApprovalPR
    Private p_ofrmARCBPostPR As frmARPaymentApprovalPR
    Private p_ofrmARDBPostPR As frmARPaymentApprovalPR

    Private p_ofrmARMPRegPR As frmARPaymentRegPR
    Private p_ofrmARCBRegPR As frmARPaymentRegPR
    Private p_ofrmARDBRegPR As frmARPaymentRegPR
    Private p_ofrmCarPaymentPRReg As frmCarPaymentPRReg
    Private p_ofrmLRPaymentRegPR As frmLRPaymentRegPR
    Private p_ofrmMPPaymentRegPR As frmMPPaymentRegPR

    Private p_ofrmPaymentCenter As frmPaymentPartners
    Private p_ofrmLRAppTransfer As frmLRApplicationTransfer

    Private p_ofrmMarketplaceCreditApp As frmMarketplace
    Private p_ofrmMarketplaceCreditAppHistory As frmMarketplaceHistory

    Private Sub EntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem.Click
        showModalForm(frmLRApplicationEntry, Me)
    End Sub

    Private Sub ApprovalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovalToolStripMenuItem.Click
        showModalForm(frmLRApplicationApproval, Me)
    End Sub

    Private Sub ReleaseToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReleaseToolStripMenuItem1.Click
        showModalForm(frmLRApplicationRelease, Me)
    End Sub

    Private Sub EntryToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem2.Click
        'showModalForm(frmLRPaymentEntry, Me)
        If p_ofrmLREntry Is Nothing Then
            p_ofrmLREntry = New frmLRPaymentEntry
            p_ofrmLREntry.LoanType = "0"
        End If
        showModalForm(p_ofrmLREntry, Me)
        p_ofrmLREntry = Nothing
    End Sub

    Private Sub PostingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingToolStripMenuItem.Click
        'showModalForm(frmLRPaymentPosting, Me)
        If p_ofrmLRPosting Is Nothing Then
            p_ofrmLRPosting = New frmLRPaymentPosting
            p_ofrmLRPosting.LoanType = "0"
        End If
        showModalForm(p_ofrmLRPosting, Me)
        p_ofrmLRPosting = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem1.Click
        showModalForm(frmLRAdjustmentEntry, Me)
    End Sub

    Private Sub ApprovalToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovalToolStripMenuItem1.Click
        showModalForm(frmLRAdjustmentApproval, Me)
    End Sub

    Private Sub LRApplicationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRApplicationToolStripMenuItem1.Click
        showModalForm(frmLRApplicationReg, Me)
    End Sub

    Private Sub LRPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRPaymentToolStripMenuItem.Click
        'showModalForm(frmLRPaymentReg, Me)
        If p_ofrmLrPaymentReg Is Nothing Then
            p_ofrmLrPaymentReg = New frmLRPaymentReg
            p_ofrmLrPaymentReg.LoanType = "0"
        End If
        showModalForm(p_ofrmLrPaymentReg, Me)
        p_ofrmLrPaymentReg = Nothing
    End Sub

    Private Sub LRAdjustmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRAdjustmentToolStripMenuItem.Click
        showModalForm(frmLRAdjustmentReg, Me)
    End Sub

    Private Sub ActiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub StandardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StandardToolStripMenuItem.Click
        Dim loReport As ggcLRReports.clsLRRep
        Dim loFrm As frmReportViewer

        loReport = New ggcLRReports.clsLRRep(p_oAppDriver)
        If loReport.ShowReport() Then
            loFrm = New frmReportViewer
            loFrm.ReportDocument = loReport.ReportSource
            loFrm.MdiParent = Me
            loFrm.Show()
        End If

    End Sub

    Private Sub ClosedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub EntryToolStripMenuItem5_Click(sender As System.Object, e As System.EventArgs) Handles EntryToolStripMenuItem5.Click
    '    If p_ofrmARCBEntry Is Nothing Then
    '        p_ofrmARCBEntry = New frmARPaymentEntry
    '        p_ofrmARCBEntry.TranType = "3"
    '    End If
    '    showModalForm(p_ofrmARCBEntry, Me)
    '    p_ofrmARCBEntry = Nothing
    'End Sub

    'Private Sub EntryToolStripMenuItem6_Click(sender As System.Object, e As System.EventArgs) Handles EntryToolStripMenuItem6.Click
    '    If p_ofrmARDBEntry Is Nothing Then
    '        p_ofrmARDBEntry = New frmARPaymentEntry
    '        p_ofrmARDBEntry.TranType = "4"
    '    End If
    '    showModalForm(p_ofrmARDBEntry, Me)
    '    p_ofrmARDBEntry = Nothing
    'End Sub

    'Private Sub PostingConfirmationToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem1.Click
    '    If p_ofrmARCBPost Is Nothing Then
    '        p_ofrmARCBPost = New frmARPaymentApproval
    '        p_ofrmARCBPost.TranType = "3"
    '    End If
    '    showModalForm(p_ofrmARCBPost, Me)
    '    p_ofrmARCBPost = Nothing
    'End Sub

    'Private Sub PostingConfirmationToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem2.Click
    '    If p_ofrmARDBPost Is Nothing Then
    '        p_ofrmARDBPost = New frmARPaymentApproval
    '        p_ofrmARDBPost.TranType = "4"
    '    End If
    '    showModalForm(p_ofrmARDBPost, Me)
    '    p_ofrmARDBPost = Nothing
    'End Sub

    'Private Sub CashBalanceToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CashBalanceToolStripMenuItem.Click
    '    If p_ofrmARCBReg Is Nothing Then
    '        p_ofrmARCBReg = New frmARPaymentReg
    '        p_ofrmARCBReg.TranType = "3"
    '    End If
    '    showModalForm(p_ofrmARCBReg, Me)
    '    p_ofrmARCBReg = Nothing
    'End Sub

    'Private Sub DownBalanceToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles DownBalanceToolStripMenuItem1.Click
    '    If p_ofrmARDBReg Is Nothing Then
    '        p_ofrmARDBReg = New frmARPaymentReg
    '        p_ofrmARDBReg.TranType = "4"
    '    End If
    '    showModalForm(p_ofrmARDBReg, Me)
    '    p_ofrmARDBReg = Nothing
    'End Sub

    'Private Sub CashBalanceToolStripMenuItem2_Click(sender As System.Object, e As System.EventArgs) Handles CashBalanceToolStripMenuItem2.Click
    '    If p_ofrmARCBRegPR Is Nothing Then
    '        p_ofrmARCBRegPR = New frmARPaymentRegPR
    '        p_ofrmARCBRegPR.TranType = "3"
    '    End If
    '    showModalForm(p_ofrmARCBRegPR, Me)
    '    p_ofrmARCBRegPR = Nothing
    'End Sub

    'Private Sub DownBalanceToolStripMenuItem3_Click(sender As System.Object, e As System.EventArgs) Handles DownBalanceToolStripMenuItem3.Click
    '    If p_ofrmARDBRegPR Is Nothing Then
    '        p_ofrmARDBRegPR = New frmARPaymentRegPR
    '        p_ofrmARDBRegPR.TranType = "4"
    '    End If
    '    showModalForm(p_ofrmARDBRegPR, Me)
    '    p_ofrmARDBRegPR = Nothing
    'End Sub

    Private Sub LRPaymentToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRPaymentToolStripMenuItem3.Click
        'showModalForm(frmLRPaymentRegPR, Me)
        If p_ofrmLRPaymentRegPR Is Nothing Then
            p_ofrmLRPaymentRegPR = New frmLRPaymentRegPR
            p_ofrmLRPaymentRegPR.LoanType = "0"
        End If
        showModalForm(p_ofrmLRPaymentRegPR, Me)
        p_ofrmLRPaymentRegPR = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem7.Click
        'showModalForm(frmLRPaymentEntryPR, Me)
        If p_ofrmLRPaymentEntryPR Is Nothing Then
            p_ofrmLRPaymentEntryPR = New frmLRPaymentEntryPR
            p_ofrmLRPaymentEntryPR.LoanType = "0"
        End If
        showModalForm(p_ofrmLRPaymentEntryPR, Me)
        p_ofrmLRPaymentEntryPR = Nothing
    End Sub

    Private Sub ConfirmationPostingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmationPostingToolStripMenuItem.Click
        'showModalForm(frmLRPaymentPostingPR, Me)
        If p_ofrmLRPaymentPostingPR Is Nothing Then
            p_ofrmLRPaymentPostingPR = New frmLRPaymentPostingPR
            p_ofrmLRPaymentPostingPR.LoanType = "0"
        End If
        showModalForm(p_ofrmLRPaymentPostingPR, Me)
        p_ofrmLRPaymentPostingPR = Nothing
    End Sub

    'Private Sub EntryToolStripMenuItem9_Click(sender As System.Object, e As System.EventArgs) Handles EntryToolStripMenuItem9.Click
    '    If p_ofrmARCBEntryPR Is Nothing Then
    '        p_ofrmARCBEntryPR = New frmARPaymentEntryPR
    '        p_ofrmARCBEntryPR.TranType = "3"
    '    End If
    '    showModalForm(p_ofrmARCBEntryPR, Me)
    '    p_ofrmARCBEntryPR = Nothing
    'End Sub

    'Private Sub EntryToolStripMenuItem10_Click(sender As System.Object, e As System.EventArgs) Handles EntryToolStripMenuItem10.Click
    '    If p_ofrmARDBEntryPR Is Nothing Then
    '        p_ofrmARDBEntryPR = New frmARPaymentEntryPR
    '        p_ofrmARDBEntryPR.TranType = "4"
    '    End If
    '    showModalForm(p_ofrmARDBEntryPR, Me)
    '    p_ofrmARDBEntryPR = Nothing
    'End Sub

    'Private Sub PostingConfirmationToolStripMenuItem4_Click(sender As System.Object, e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem4.Click
    '    If p_ofrmARCBPostPR Is Nothing Then
    '        p_ofrmARCBPostPR = New frmARPaymentApprovalPR
    '        p_ofrmARCBPostPR.TranType = "3"
    '    End If
    '    showModalForm(p_ofrmARCBPostPR, Me)
    '    p_ofrmARCBPostPR = Nothing
    'End Sub

    'Private Sub PostingConfirmationToolStripMenuItem5_Click(sender As System.Object, e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem5.Click
    '    If p_ofrmARDBPostPR Is Nothing Then
    '        p_ofrmARDBPostPR = New frmARPaymentApprovalPR
    '        p_ofrmARDBPostPR.TranType = "4"
    '    End If
    '    showModalForm(p_ofrmARDBPostPR, Me)
    '    p_ofrmARDBPostPR = Nothing
    'End Sub

    Private Sub ARPaymentToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ARPaymentToolStripMenuItem2.Click
        If p_ofrmARMPRegPR Is Nothing Then
            p_ofrmARMPRegPR = New frmARPaymentRegPR
            p_ofrmARMPRegPR.TranType = "2"
        End If
        showModalForm(p_ofrmARMPRegPR, Me)
        p_ofrmARMPRegPR = Nothing
    End Sub

    Private Sub ARPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ARPaymentToolStripMenuItem.Click
        If p_ofrmARMPReg Is Nothing Then
            p_ofrmARMPReg = New frmARPaymentReg
            p_ofrmARMPReg.TranType = "2"
        End If
        showModalForm(p_ofrmARMPReg, Me)
        p_ofrmARMPReg = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem5.Click
        If p_ofrmARMPEntryPR Is Nothing Then
            p_ofrmARMPEntryPR = New frmARPaymentEntryPR
            p_ofrmARMPEntryPR.TranType = "2"
        End If
        showModalForm(p_ofrmARMPEntryPR, Me)
        p_ofrmARMPEntryPR = Nothing
    End Sub

    Private Sub ConfirmationPostingToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmationPostingToolStripMenuItem1.Click
        If p_ofrmARMPPostPR Is Nothing Then
            p_ofrmARMPPostPR = New frmARPaymentApprovalPR
            p_ofrmARMPPostPR.TranType = "2"
        End If
        showModalForm(p_ofrmARMPPostPR, Me)
        p_ofrmARMPPostPR = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem3.Click
        If p_ofrmARMPEntry Is Nothing Then
            p_ofrmARMPEntry = New frmARPaymentEntry
            p_ofrmARMPEntry.TranType = "2"
        End If
        showModalForm(p_ofrmARMPEntry, Me)
        p_ofrmARMPEntry = Nothing
    End Sub

    Private Sub ConfirmationPostingToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmationPostingToolStripMenuItem2.Click
        If p_ofrmARMPPost Is Nothing Then
            p_ofrmARMPPost = New frmARPaymentApproval
            p_ofrmARMPPost.TranType = "2"
        End If
        showModalForm(p_ofrmARMPPost, Me)
        p_ofrmARMPPost = Nothing
    End Sub

    Private Sub ChecksReceivedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChecksReceivedToolStripMenuItem.Click
        showModalForm(frmCheckReg, Me)
    End Sub

    Private Sub EntryToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem4.Click
        If p_ofrmCarPaymentEntry Is Nothing Then
            p_ofrmCarPaymentEntry = New frmCarPaymentEntry
            p_ofrmCarPaymentEntry.LoanType = "1"
        End If
        showModalForm(p_ofrmCarPaymentEntry, Me)
        p_ofrmCarPaymentEntry = Nothing
    End Sub

    Private Sub PostingConfirmationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem.Click
        If p_ofrmCarPaymentPosting Is Nothing Then
            p_ofrmCarPaymentPosting = New frmCarPaymentPosting
            p_ofrmCarPaymentPosting.LoanType = "1"
        End If
        showModalForm(p_ofrmCarPaymentPosting, Me)
        p_ofrmCarPaymentPosting = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem6.Click
        If p_ofrmCarPaymentPR Is Nothing Then
            p_ofrmCarPaymentPR = New frmCarPaymentPR
            p_ofrmCarPaymentPR.LoanType = "1"
        End If
        showModalForm(p_ofrmCarPaymentPR, Me)
        p_ofrmCarPaymentPR = Nothing
    End Sub

    Private Sub PostingConfirmationToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem1.Click
        If p_ofrmCarPostingPR Is Nothing Then
            p_ofrmCarPostingPR = New frmCarPaymentPostingPr
            p_ofrmCarPostingPR.LoanType = "1"
        End If
        showModalForm(p_ofrmCarPostingPR, Me)
        p_ofrmCarPostingPR = Nothing
    End Sub

    Private Sub ActiveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CarCheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarCheckToolStripMenuItem.Click
        showModalForm(frmCarCheckClearing, Me)
    End Sub

    Private Sub LRMCCheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRMCCheckToolStripMenuItem.Click
        showModalForm(frmCheckClearing, Me)
    End Sub

    Private Sub CarPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarPaymentToolStripMenuItem1.Click
        If p_ofrmCarPaymentReg Is Nothing Then
            p_ofrmCarPaymentReg = New frmCarPaymentReg
            p_ofrmCarPaymentReg.LoanType = "1"
        End If
        showModalForm(p_ofrmCarPaymentReg, Me)
        p_ofrmCarPaymentReg = Nothing
    End Sub

    Private Sub CarPaymentToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarPaymentToolStripMenuItem2.Click
        If p_ofrmCarPaymentPRReg Is Nothing Then
            p_ofrmCarPaymentPRReg = New frmCarPaymentPRReg
            p_ofrmCarPaymentPRReg.LoanType = "1"
        End If
        showModalForm(p_ofrmCarPaymentPRReg, Me)
        p_ofrmCarPaymentPRReg = Nothing

    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        If MsgBox("Are you sure to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub EntryToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem8.Click
        If p_ofrmMPPaymentEntry Is Nothing Then
            p_ofrmMPPaymentEntry = New frmMPPaymentEntry
            p_ofrmMPPaymentEntry.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentEntry, Me)
        p_ofrmMPPaymentEntry = Nothing
    End Sub

    Private Sub PostingConfirmationToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem2.Click
        If p_ofrmMPPaymentPosting Is Nothing Then
            p_ofrmMPPaymentPosting = New frmMPPaymentPosting
            p_ofrmMPPaymentPosting.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentPosting, Me)
        p_ofrmMPPaymentPosting = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem9.Click
        If p_ofrmMPPaymentEntryPR Is Nothing Then
            p_ofrmMPPaymentEntryPR = New frmMPPaymentEntryPR
            p_ofrmMPPaymentEntryPR.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentEntryPR, Me)
        p_ofrmMPPaymentEntryPR = Nothing
    End Sub

    Private Sub PostingConfirmationToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingConfirmationToolStripMenuItem3.Click
        If p_ofrmMPPPaymentPostingPR Is Nothing Then
            p_ofrmMPPPaymentPostingPR = New frmMPPaymentPRPosting
            p_ofrmMPPPaymentPostingPR.TranType = "2"
        End If
        showModalForm(p_ofrmMPPPaymentPostingPR, Me)
        p_ofrmMPPPaymentPostingPR = Nothing
    End Sub

    Private Sub MPPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPPaymentToolStripMenuItem1.Click
        If p_ofrmMPPaymentEntryReg Is Nothing Then
            p_ofrmMPPaymentEntryReg = New frmMPPaymentEntryReg
            p_ofrmMPPaymentEntryReg.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentEntryReg, Me)
        p_ofrmMPPaymentEntryReg = Nothing
    End Sub

    Private Sub MPPaymentToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPPaymentToolStripMenuItem2.Click
        If p_ofrmMPPaymentRegPR Is Nothing Then
            p_ofrmMPPaymentRegPR = New frmMPPaymentRegPR
            p_ofrmMPPaymentRegPR.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentRegPR, Me)
        p_ofrmMPPaymentRegPR = Nothing
    End Sub

    Private Sub MPPaymentToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loRpt As clsMPPayment
        loRpt = New clsMPPayment(p_oAppDriver)

        If loRpt.getParameter() Then
            Call loRpt.ReportTrans()
        End If
    End Sub

    Private Sub LRAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRAccountToolStripMenuItem.Click
        If p_ofrmLRActive Is Nothing Then
            p_ofrmLRActive = New frmLRMasterReg
            p_ofrmLRActive.Status = 0
        End If
        showModalForm(p_ofrmLRActive, Me)
        p_ofrmLRActive = Nothing
    End Sub

    Private Sub LRAccountToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRAccountToolStripMenuItem2.Click
        If p_ofrmLRClosed Is Nothing Then
            p_ofrmLRClosed = New frmLRMasterReg
            p_ofrmLRClosed.Status = 1234
        End If
        showModalForm(p_ofrmLRClosed, Me)
        p_ofrmLRClosed = Nothing
    End Sub

    Private Sub CarAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarAccountToolStripMenuItem.Click
        If p_frmLoanView Is Nothing Then
            p_frmLoanView = New frmAutoARReg
            p_frmLoanView.AccountStatus = "-1"
            'p_frmLoanManagement.AccountStatus = "10234"
        End If
        showModalForm(p_frmLoanView, Me)
        p_frmLoanView = Nothing
    End Sub

    Private Sub CarAccountToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarAccountToolStripMenuItem1.Click
        If p_frmLoanView Is Nothing Then
            p_frmLoanView = New frmAutoARReg
            p_frmLoanView.AccountStatus = "1"
            'p_frmLoanManagement.AccountStatus = "10234"
        End If
        showModalForm(p_frmLoanView, Me)
        p_frmLoanView = Nothing
    End Sub

    Private Sub MCAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCAccountToolStripMenuItem.Click
        'If p_ofrmMCActive Is Nothing Then
        '    p_ofrmMCActive = New frmMCMasterReg
        '    p_ofrmMCActive.Status = 0
        'End If
        'showModalForm(p_ofrmMCActive, Me)
        'p_ofrmMCActive = Nothing
    End Sub

    Private Sub MCAccountToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCAccountToolStripMenuItem1.Click
        'If p_ofrmMCClosed Is Nothing Then
        '    p_ofrmMCClosed = New frmMCMasterReg
        '    p_ofrmMCClosed.Status = 1234
        'End If
        'showModalForm(p_ofrmMCClosed, Me)
        'p_ofrmMCClosed = Nothing
    End Sub

    Private Sub MPAccountToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPAccountToolStripMenuItem1.Click
        'If p_ofrmMPCLosed Is Nothing Then
        '    p_ofrmMPCLosed = New frmMPMasterReg
        '    p_ofrmMPCLosed.Status = 1234
        'End If
        'showModalForm(p_ofrmMPCLosed, Me)
        'p_ofrmMPCLosed = Nothing
    End Sub

    Private Sub MPAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPAccountToolStripMenuItem.Click
        'If p_ofrmMPActive Is Nothing Then
        '    p_ofrmMPActive = New frmMPMasterReg
        '    p_ofrmMPActive.Status = 0
        'End If
        'showModalForm(p_ofrmMPActive, Me)
        'p_ofrmMPActive = Nothing
    End Sub

    Private Sub MPCreditApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPCreditApplicationToolStripMenuItem.Click
        showModalForm(frmMPCreditApp, Me)
    End Sub

    Private Sub LoanApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoanApplicationToolStripMenuItem.Click

    End Sub

    Private Sub MCCreditApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCCreditApplicationToolStripMenuItem.Click
        showModalForm(frmMCCreditApp, Me)
    End Sub

    Private Sub MCModelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MCModelToolStripMenuItem.Click
        showModalForm(frmMCModel, Me)
    End Sub

    Private Sub RedeemableItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedeemableItemToolStripMenuItem.Click
        showModalForm(frmRedeemableItems, Me)
    End Sub

    Private Sub CreditApplicationQMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showModalForm(frmQuickMatch, Me)
    End Sub

    Private Sub CSSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If p_ofrmCollection Is Nothing Then
            p_ofrmCollection = New frmMCCreditAppReview
        End If
        showModalForm(p_ofrmCollection, Me)
        p_ofrmCollection = Nothing
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'mac 2021-06-14
        '   refresh sysmonitor regardless the system is not idle since the procedure was a Thread
        'If LCase(p_oAppDriver.ProductID) <> "integsys" And p_sEvaluator <> "" Then
        '    p_nCtr += 1
        '    If p_nCtr >= pxeMONITOR_INTERVAL Then
        '        If p_bShowd Then
        '            Call loadMonitor()
        '        Else
        '            Call showMonitor()
        '        End If
        '        p_nCtr = 0
        '    End If
        'End If

        'old code for refreshing sysmonitor
        'requires the pc to be idle for 30 second to refresh the sysmonitor
        If LCase(p_oAppDriver.ProductID) <> "integsys" And p_sEvaluator <> "" Then
            If p_nLocation <> Cursor.Position Then
                If p_bRunning Then
                    p_bRunning = False
                End If
                p_nLocation = Cursor.Position
                p_nIdleTIme = Date.Now
            ElseIf Not p_bRunning AndAlso (Date.Now - p_nIdleTIme).TotalSeconds > 30 Then
                If p_bShowd Then
                    Call loadMonitor()
                Else
                    Call showMonitor()
                End If
                p_bRunning = True
            End If
        End If
    End Sub

    Private Sub mdiMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Not p_bLoaded Then
            p_bLoaded = True
            If LCase(p_oAppDriver.ProductID) <> "integsys" And p_sEvaluator <> "" Then
                Call showMonitor()
            End If
        End If
    End Sub

    Private Sub mdiMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        p_oAppDriver.MDI = Me
        frmMonitor.MdiParent = p_oAppDriver.MDI

        p_isEvaluator = modMain.p_isEvaluator
        Select Case p_isEvaluator
            Case "0"
                EvaluatorToolStripMenuItem.Visible = False
                CreditApplicationMarketplaceToolStripMenuItem.Visible = False
            Case "1"
                CollectorToolStripMenuItem.Visible = False
            Case ""
                EvaluatorToolStripMenuItem.Visible = False
                CollectorToolStripMenuItem.Visible = False
                TabletToolStripMenuItem.Visible = False
                LoanApplicationToolStripMenuItem.Visible = False
                ToolStripSeparator44.Visible = False
                CreditApplicationMarketplaceToolStripMenuItem.Visible = False
        End Select

        'mac 2020-07-25
        PaymentPartnersToolStripMenuItem.Visible = p_oAppDriver.ProductID.ToLower = "lrtrackr"
        ToolStripSeparator43.Visible = p_oAppDriver.ProductID.ToLower = "lrtrackr"
        LRApplicationTransferToolStripMenuItem.Visible = p_sEvaluator = "False"
        'end - mac 2020-07-25
    End Sub

    Private Sub EvaluatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EvaluatorToolStripMenuItem.Click
        If p_ofrmEvaluator Is Nothing Then
            p_ofrmEvaluator = New frmMCCreditAppCategorization
        End If
        showModalForm(p_ofrmEvaluator, Me)
        p_ofrmEvaluator = Nothing
    End Sub

    Private Sub CollectorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CollectorToolStripMenuItem.Click
        If p_ofrmCollection Is Nothing Then
            p_ofrmCollection = New frmMCCreditAppReview
        End If
        showModalForm(p_ofrmCollection, Me)
        p_ofrmCollection = Nothing
    End Sub

    Private Sub PaymentPartnersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentPartnersToolStripMenuItem.Click
        If p_ofrmPaymentCenter Is Nothing Then
            p_ofrmPaymentCenter = New frmPaymentPartners
        End If
        showModalForm(p_ofrmPaymentCenter, Me)
        p_ofrmPaymentCenter = Nothing
    End Sub

    Private Sub TabletToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabletToolStripMenuItem.Click
        If p_ofrmCreditOverride Is Nothing Then
            p_ofrmCreditOverride = New frmMCCreditAppOverride
        End If
        showModalForm(p_ofrmCreditOverride, Me)
        p_ofrmCreditOverride = Nothing
    End Sub

    Private Sub CarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarToolStripMenuItem.Click
        showModalForm(frmCarApplicationHistory, Me)
    End Sub

    Private Sub MotorcycleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MotorcycleToolStripMenuItem.Click
        showModalForm(frmMCCreditAppHistory, Me)
    End Sub

    Private Sub MobilePhoneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MobilePhoneToolStripMenuItem.Click
        showModalForm(frmMPCreditAppHistory, Me)
    End Sub

    Private Sub LRApplicationTransferToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRApplicationTransferToolStripMenuItem.Click
        showModalForm(frmLRApplicationTransfer, Me)
    End Sub

    Private Sub CreditAppVerificationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showModalForm(FrmCITagging, Me)
    End Sub

    Private Sub CreditApplicationMarketplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditApplicationMarketplaceToolStripMenuItem.Click
        If p_ofrmMarketplaceCreditApp Is Nothing Then
            p_ofrmMarketplaceCreditApp = New frmMarketplace
        End If
        showModalForm(p_ofrmMarketplaceCreditApp, Me)
        p_ofrmMarketplaceCreditApp = Nothing
    End Sub

    Private Sub MarketplaceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarketplaceToolStripMenuItem.Click
        If p_ofrmMarketplaceCreditAppHistory Is Nothing Then
            p_ofrmMarketplaceCreditAppHistory = New frmMarketplaceHistory
        End If
        showModalForm(p_ofrmMarketplaceCreditAppHistory, Me)
        p_ofrmMarketplaceCreditAppHistory = Nothing
    End Sub
End Class
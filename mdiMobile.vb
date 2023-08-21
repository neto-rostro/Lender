Imports ggcAppDriver
Imports System.Globalization

Public Class mdiMobile
    Private p_ofrmMPPaymentEntry As frmMPPaymentEntry
    Private p_ofrmMPPaymentPosting As frmMPPaymentPosting
    Private p_ofrmMPPaymentEntryPR As frmMPPaymentEntryPR
    Private p_ofrmMPPPaymentPostingPR As frmMPPaymentPRPosting
    Private p_ofrmMPPaymentRegPR As frmMPPaymentRegPR
    Private p_ofrmMPPaymentEntryReg As frmMPPaymentEntryReg

    Private Sub MPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPToolStripMenuItem.Click
        If p_ofrmMPPaymentEntry Is Nothing Then
            p_ofrmMPPaymentEntry = New frmMPPaymentEntry
            p_ofrmMPPaymentEntry.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentEntry, Me)
        p_ofrmMPPaymentEntry = Nothing
    End Sub

    Private Sub mdiMobile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PostingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingToolStripMenuItem.Click
        If p_ofrmMPPaymentPosting Is Nothing Then
            p_ofrmMPPaymentPosting = New frmMPPaymentPosting
            p_ofrmMPPaymentPosting.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentPosting, Me)
        p_ofrmMPPaymentPosting = Nothing
    End Sub

    Private Sub EntryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EntryToolStripMenuItem.Click
        If p_ofrmMPPaymentEntryPR Is Nothing Then
            p_ofrmMPPaymentEntryPR = New frmMPPaymentEntryPR
            p_ofrmMPPaymentEntryPR.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentEntryPR, Me)
        p_ofrmMPPaymentEntryPR = Nothing
    End Sub

    Private Sub PostingToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostingToolStripMenuItem1.Click
        If p_ofrmMPPPaymentPostingPR Is Nothing Then
            p_ofrmMPPPaymentPostingPR = New frmMPPaymentPRPosting
            p_ofrmMPPPaymentPostingPR.TranType = "2"
        End If
        showModalForm(p_ofrmMPPPaymentPostingPR, Me)
        p_ofrmMPPPaymentPostingPR = Nothing
    End Sub

    Private Sub MPCheckToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPCheckToolStripMenuItem.Click
        showModalForm(frmCheckClearing, Me)
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        If MsgBox("Are you sure to logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "InHouse Financing System ") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub MPPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MPPaymentToolStripMenuItem.Click
        If p_ofrmMPPaymentEntryReg Is Nothing Then
            p_ofrmMPPaymentEntryReg = New frmMPPaymentEntryReg
            p_ofrmMPPaymentEntryReg.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentEntryReg, Me)
        p_ofrmMPPaymentEntryReg = Nothing
    End Sub

    Private Sub ProvisionaryReceiptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProvisionaryReceiptToolStripMenuItem.Click
        If p_ofrmMPPaymentRegPR Is Nothing Then
            p_ofrmMPPaymentRegPR = New frmMPPaymentRegPR
            p_ofrmMPPaymentRegPR.TranType = "2"
        End If
        showModalForm(p_ofrmMPPaymentRegPR, Me)
        p_ofrmMPPaymentRegPR = Nothing
    End Sub

    Private Sub ChecksReceivedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChecksReceivedToolStripMenuItem.Click
        showModalForm(frmCheckReg, Me)
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

    Private Sub MobileAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MobileAccountToolStripMenuItem.Click
        If MsgBox("Are you sure to exit?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "In-House Financing System ") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class
Imports ggcAppDriver
Imports System.Globalization

Public Class mdiLender
    Private p_frmLoanManagement As frmLoanManagement
    Private p_frmCarSerial As frmCarSerial


    Private Sub LoanManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoanManagementToolStripMenuItem.Click
        showModalForm(frmLoanManagement, Me)
    End Sub

    Private Sub CarTradeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CarTradeToolStripMenuItem.Click
        showModalForm(frmCarSerial, Me)
    End Sub

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub mdiLender_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
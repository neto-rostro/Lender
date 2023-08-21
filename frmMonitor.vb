Imports CrystalDecisions.CrystalReports.Engine
Public Class frmMonitor
    Dim p_oFormTrans As frmMCCreditAppReview
    Dim p_oFormEvaluate As frmMCCreditAppCategorization
    Dim selRow As Integer
    Dim selCol As Integer
    Private p_cEvaluator As String

    Public Property isEvaluator() As String
        Get
            Return p_cEvaluator
        End Get
        Set(ByVal value As String)
            p_cEvaluator = value
        End Set
    End Property

    Private Sub tbcMon_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcMon.SelectedIndexChanged
        Select Case tbcMon.SelectedIndex
            Case 0
                Label1.Text = "Credit Online Application Selection"
        End Select
    End Sub

    Private Sub frmMonitor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Select Case tbcMon.SelectedIndex
            Case 0
                Label1.Text = "Credit Online Application for Selection"
        End Select
    End Sub


    Private Sub dgvCreditApp_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvCreditApp.DoubleClick
        selRow = dgvCreditApp.CurrentCell.RowIndex
        selCol = 0
        If p_cEvaluator = "0" Then
            If Trim(dgvCreditApp.Rows(dgvCreditApp.Rows.Count - 1).Cells(7).Value) <> "" Then
                p_oFormTrans = New frmMCCreditAppReview
                With dgvCreditApp
                    p_oFormTrans.sTransNox = .Rows(dgvCreditApp.CurrentRow.Index).Cells(7).Value
                    p_oFormTrans.Show()
                End With
            End If
        End If
    End Sub

    Private Sub dgvCreditApp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCreditApp.CellContentClick

    End Sub
End Class
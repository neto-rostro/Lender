Public Class frmMPCreditAppReg
    Public Sub initgrid()
        With dgvDetail
            .ColumnCount = 5
            .RowCount = 1
            .Columns(0).Name = "No"
            .Columns(1).Name = "Client Name"
            .Columns(2).Name = "Address"
            .Columns(3).Name = "Mobile No"
            .Columns(4).Name = "Relationship"

            .Columns(0).Width = 55
            .Columns(1).Width = 220
            .Columns(2).Width = 220
            .Columns(3).Width = 130
            .Columns(4).Width = 170

            .Columns(0).Resizable = DataGridViewTriState.False
            .Columns(1).Resizable = DataGridViewTriState.False
            .Columns(2).Resizable = DataGridViewTriState.False
            .Columns(3).Resizable = DataGridViewTriState.False
            .Columns(4).Resizable = DataGridViewTriState.False

            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With

    End Sub

    Private Sub frmCreditAppReg_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Call initgrid()
    End Sub
End Class
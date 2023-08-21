Imports ggcAppDriver
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmDateCriteria
    Private p_oApp As GRider
    Private poControl As Control

    Private p_nButton As Integer
    Private pnLoadx As Integer

    Public WriteOnly Property AppDriver() As GRider
        Set(ByVal value As GRider)
            p_oApp = value
        End Set
    End Property

    Public ReadOnly Property isOkey() As Boolean
        Get
            Return p_nButton = 0
        End Get
    End Property


    Private Sub frmDateCriteria_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmDateCriteria_Activated")
        If pnLoadx = 1 Then
            txtDateFrom.Focus()
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmDateCriteria_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Enter
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Enter
                        SetNextFocus()
                    Case Keys.Up
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub frmDateCriteria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmDateCriteria_Load")

        txtDateFrom.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
        txtDateThru.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Ok
                If isEntryOk() Then
                    p_nButton = 0
                    Me.Hide()
                End If
            Case 2 ' Cancel Update
                p_nButton = 1
                Me.Hide()
        End Select
    End Sub

    Private Function isEntryOk() As Boolean
        If Not IsDate(txtDateFrom.Text) Then
            MsgBox("Invalid Date Detected!", vbOKOnly, "Date Criteria")
            txtDateFrom.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
            Return False
        End If

        If Not IsDate(txtDateThru.Text) Then
            MsgBox("Invalid Date Detected!", vbOKOnly, "Date Criteria")
            txtDateThru.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
            Return False
        End If

        If CDate(txtDateThru.Text) < CDate(txtDateFrom.Text) Then
            MsgBox("Invalid Date Range Detected!", vbOKOnly, "Date Criteria")
            Return False
        End If

        Return True
    End Function

    Private Sub txtDateFrom_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateFrom.GotFocus
        txtDateFrom.BackColor = Color.LemonChiffon
        If Not IsDate(txtDateFrom.Text) Then
            txtDateFrom.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
        Else
            txtDateFrom.Text = Format(CDate(txtDateFrom.Text), "yyyy-MM-dd")
        End If
    End Sub

    Private Sub txtDateFrom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDateFrom.KeyDown

    End Sub

    Private Sub txtDateThru_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateThru.GotFocus
        txtDateThru.BackColor = Color.LemonChiffon
        If Not IsDate(txtDateThru.Text) Then
            txtDateThru.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
        Else
            txtDateThru.Text = Format(CDate(txtDateThru.Text), "yyyy-MM-dd")
        End If
    End Sub

    Private Sub txtDateThru_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDateThru.KeyDown

    End Sub

    Private Sub txtDateFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateFrom.LostFocus
        txtDateFrom.BackColor = Color.White
        If Not IsDate(txtDateFrom.Text) Then
            txtDateFrom.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
        Else
            txtDateFrom.Text = Format(CDate(txtDateFrom.Text), "yyyy-MM-dd")
        End If
    End Sub

    Private Sub txtDateThru_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateThru.LostFocus
        txtDateThru.BackColor = Color.White
        If Not IsDate(txtDateThru.Text) Then
            txtDateThru.Text = Format(CDate(p_oApp.SysDate), "yyyy-MM-dd")
        Else
            txtDateThru.Text = Format(CDate(txtDateThru.Text), "yyyy-MM-dd")
        End If
    End Sub

    Private Sub cmdButtn00_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButtn00.Click
        If isEntryOk() Then
            p_nButton = 0
            Me.Hide()
        End If
    End Sub

    Private Sub cmdButtn01_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButtn01.Click
        p_nButton = 1
        Me.Hide()
    End Sub
End Class
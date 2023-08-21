'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'    MP Payment Approval Form(PR)
'
' Copyright 2016 and Beyond
' All Rights Reserved
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
' €  All  rights reserved. No part of this  software  €€  This Software is Owned by        €
' €  may be reproduced or transmitted in any form or  €€                                   €
' €  by   any   means,  electronic   or  mechanical,  €€    GUANZON MERCHANDISING CORP.    €
' €  including recording, or by information  storage  €€     Guanzon Bldg. Perez Blvd.     €
' €  and  retrieval  systems, without  prior written  €€           Dagupan City            €
' €  from the author.                                 €€  Tel No. 522-1085 ; 522-9275      €
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
'
' ==========================================================================================
'  Kalyptus [ 07/14/2017 10:51 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmMPPaymentPRPosting
    Private WithEvents poTrans As ARPayment_PR_MP
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_cTranType As String

    Public Property TranType() As String
        Get
            Return p_cTranType
        End Get
        Set(ByVal value As String)
            p_cTranType = value
        End Set
    End Property

    Private Sub frmMPPaymentPRPosting_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMPPaymentPRPosting_Activated")
        If pnLoadx = 1 Then

            Me.Text = "MP Payment Posting(PR)"

            Select Case p_cTranType
                Case "2"
                    Me.Text = Me.Text & " - Monthly Payment"
                Case "3"
                    Me.Text = Me.Text & " - Cash Balance"
                Case "4"
                    Me.Text = Me.Text & " - Down Balance"
            End Select

            poTrans = New ARPayment_PR_MP(p_oAppDriver, 0, p_cTranType)

            Call loadMaster(Me.Panel1)
            Call loadMaster(Me.Panel5)
            Call loadMaster(Me.Panel2)

            txtSeeks00.Text = poTrans.BranchName
            If p_oAppDriver.ProductID <> "LRTrackr" Then
                txtSeeks00.ReadOnly = True
                txtSeeks00.TabStop = False
                txtSeeks01.Focus()
            Else
                txtSeeks00.ReadOnly = False
                txtSeeks00.TabStop = True
                txtSeeks00.Focus()
            End If

            pnLoadx = 2

        End If
    End Sub

    Private Sub frmMPPaymentPRPosting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            '*********************
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0
                        Call poTrans.SearchBranch(loTxt.Text, False, True)
                        txtSeeks00.Text = poTrans.BranchName
                    Case 1, 2
                        If poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True)) Then
                            Call loadMaster(Me.Panel1)
                            Call loadMaster(Me.Panel5)
                            Call loadMaster(Me.Panel2)
                        End If
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmMPPaymentPRPosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmMPPaymentPRPosting_Load")
        If pnLoadx = 0 Then

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster(ByVal loControl As Control)
        Dim loTxt As Control

        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMaster(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
                        Select Case loIndex
                            Case 1
                                If poTrans.EditMode <> xeEditMode.MODE_UNKNOWN Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), "MMMM dd, yyyy")
                                Else
                                    loTxt.Text = ""
                                End If
                            Case 8, 10, 11, 82 To 90
                                If poTrans.EditMode <> xeEditMode.MODE_UNKNOWN Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                ElseIf (TypeOf loTxt Is ComboBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    Dim loCombo As ComboBox = CType(loTxt, ComboBox)
                    If LCase(Mid(loTxt.Name, 1, 8)) = "cmbfield" Then
                        If poTrans.EditMode = xeEditMode.MODE_UNKNOWN Then
                            loCombo.SelectedIndex = -1
                        Else
                            loCombo.SelectedIndex = poTrans.Master(loIndex) - 2
                        End If
                    End If
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls

        If IsNumeric(txtField08.Text) And IsNumeric(txtField11.Text) Then
            txtOther01.Text = Format(CDec(txtField08.Text) + CDec(txtField11.Text), xsDECIMAL)
        Else
            txtOther01.Text = "0.00"
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
            Case 1 ' Cancel Transaction
                If poTrans.CancelTransaction Then
                    MsgBox("Transaction was cancelled successfully!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "MP Payment Posting")
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel5)
                    Call loadMaster(Me.Panel2)
                End If
            Case 2 ' Post Transaction
                If poTrans.PostTransaction Then
                    MsgBox("Transaction was posted successfully!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "MP Payment Posting")
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel5)
                    Call loadMaster(Me.Panel2)
                End If
            Case 3 ' Browse Transaction
                If Not poControl Is Nothing Then
                    Dim loTxt As TextBox
                    loTxt = CType(poControl, System.Windows.Forms.TextBox)

                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))

                    Select Case loIndex
                        Case 0
                            Call poTrans.SearchBranch(loTxt.Text, False, True)
                            txtSeeks00.Text = poTrans.BranchName
                        Case 1, 2
                            If poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True)) Then
                                Call loadMaster(Me.Panel1)
                                Call loadMaster(Me.Panel5)
                                Call loadMaster(Me.Panel2)
                            End If
                    End Select
                End If
        End Select
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
    End Sub
End Class
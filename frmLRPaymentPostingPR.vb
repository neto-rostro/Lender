'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     LR Payment Posting Form(PR)
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
'  Kalyptus [ 07/14/2017 11:11 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmLRPaymentPostingPR
    Private WithEvents poTrans As ggcLRTransaction.LRPayment_PR
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_cLoanType As String

    Public Property LoanType() As String
        Get
            Return p_cLoanType
        End Get
        Set(ByVal value As String)
            p_cLoanType = value
        End Set
    End Property

    Private Sub frmLRPaymentPostingPR_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLRPaymentPostingPR_Activated")
        If pnLoadx = 1 Then
            Call loadMaster(Me.Panel1)
            Call loadMaster(Me.Panel2)
            Call loadMaster(Me.Panel5)
            txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
            txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))

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

    Private Sub frmLRPaymentPostingPR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    Case 1
                        Call poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True))
                        Call loadMaster(Me.Panel1)
                        Call loadMaster(Me.Panel2)
                        Call loadMaster(Me.Panel5)
                        txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                        txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmLRPaymentPostingPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLRPaymentPostingPR_Load")
        If pnLoadx = 0 Then
            poTrans = New ggcLRTransaction.LRPayment_PR(p_oAppDriver, 0)
            poTrans.LoanType = p_cLoanType

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

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
                                If IsDate(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), "MMMM dd, yyyy")
                                Else
                                    loTxt.Text = ""
                                End If
                            Case 7, 8, 9, 82, 86, 87, 91
                                If IsNumeric(poTrans.Master(loIndex)) And poTrans.Master(loIndex) <> vbEmpty Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls

        If poTrans.Master("nAcctTerm") > 0 Then
            txtField83.Text = IIf(poTrans.Master("nInterest") = poTrans.Master("nIntTotal"), 0.0, poTrans.Master("nInterest") / poTrans.Master("nAcctTerm"))
        Else
            txtField83.Text = "0.00"
        End If

        If IsNumeric(txtField86.Text) And IsNumeric(txtField83.Text) Then
            txtOther01.Text = Format(CDec(txtField86.Text) + CDec(txtField83.Text), xsDECIMAL)
            txtOther02.Text = Format(poTrans.Master(82) + (poTrans.Master("nInterest") - poTrans.Master("nIntTotal")), xsDECIMAL)
        Else
            txtOther01.Text = "0.00"
            txtOther02.Text = "0.00"
        End If

        txtOther03.Text = txtField09.Text
        If IsNumeric(txtField07.Text) And IsNumeric(txtField08.Text) And IsNumeric(txtField09.Text) Then
            txtOther04.Text = Format(CDec(txtField07.Text) + CDec(txtField08.Text) + CDec(txtField09.Text), xsDECIMAL)
        Else
            txtOther04.Text = "0.00"
        End If

    End Sub

    'Handles GotFocus Events for txtField & txtField
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
    End Sub

    'Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'Dim loTxt As TextBox
        'loTxt = CType(sender, System.Windows.Forms.TextBox)

        'Dim loIndex As Integer
        'loIndex = Val(Mid(loTxt.Name, 9))
        'If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
        '    poTrans.Master(loIndex) = loTxt.Text
        'End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 1 ' Disapproved
                If poTrans.CancelTransaction Then
                    MsgBox("Payment was cancelled successfully!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "LR Payment Posting")
                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel2)
                    Call loadMaster(Me.Panel5)
                    txtSeeks01.Text = ""
                    txtSeeks02.Text = ""
                    txtSeeks01.Focus()
                End If
            Case 2 ' Approved
                'she 2020-08-22 validate entry
                If isTransValid(CDate(txtField01.Text), "MPPy", txtField03.Text, CDbl(txtField91.Text) + CDbl(txtField09.Text)) = False Then
                    MsgBox("No Reference no found from unencoded transaction!!" & vbCrLf &
                                     " Pls check your entry then try again!!!")
                    Exit Sub
                End If
                If poTrans.PostTransaction Then
                    MsgBox("Payment was posted successfully!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "LR Payment Posting")
                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel2)
                    Call loadMaster(Me.Panel5)
                    txtSeeks01.Text = ""
                    txtSeeks02.Text = ""
                    txtSeeks01.Focus()
                End If
            Case 3 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))

                If loIndex = 0 Then
                    poTrans.SearchBranch(poControl.Text, False, True)
                    txtSeeks00.Text = poTrans.BranchName
                ElseIf loIndex = 1 Or loIndex = 2 Then
                    If poTrans.SearchTransaction(poControl.Text, IIf(loIndex = 1, False, True)) Then
                        Call loadMaster(Me.Panel1)
                        Call loadMaster(Me.Panel2)
                        Call loadMaster(Me.Panel5)
                        txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                        txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))
                    End If
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

    Private Sub poTrans_MasterRetrieved(Index As Integer, Value As Object) Handles poTrans.MasterRetrieved
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 91
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 80
                loTxt.Text = Value
                txtField04.Text = poTrans.Master(4)
                txtField81.Text = poTrans.Master(81)
                txtField88.Text = poTrans.Master(88)

                txtField82.Text = Format(poTrans.Master(82), xsDECIMAL)
                txtField87.Text = Format(poTrans.Master(87), xsDECIMAL)
                txtField86.Text = Format(poTrans.Master(86), xsDECIMAL)

                If poTrans.Master("nAcctTerm") > 0 Then
                    txtField83.Text = IIf(poTrans.Master("nInterest") = poTrans.Master("nIntTotal"), 0.0, poTrans.Master("nInterest") / poTrans.Master("nAcctTerm"))
                Else
                    txtField83.Text = "0.00"
                End If

                txtOther01.Text = Format(CDec(txtField86.Text) + CDec(txtField83.Text), xsDECIMAL)
                txtOther02.Text = Format(poTrans.Master(82) + (poTrans.Master("nInterest") - poTrans.Master("nIntTotal")), xsDECIMAL)
            Case 7, 8, 9
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 1
                loTxt.Text = Format(Value, xsDATE_MEDIUM)
            Case Else
                Debug.Print(loTxt.Name)
                loTxt.Text = Value
        End Select
    End Sub
End Class
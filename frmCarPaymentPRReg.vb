'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     LR Car Payment Entry Form
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
'  Jheff [ 01/06/2018 10:57 am ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction.LRPayment_PR_Car
Imports ggcAppDriver

Public Class frmCarPaymentPRReg
    Private WithEvents poTrans As ggcLRTransaction.LRPayment_PR_Car
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

    Private Sub frmCarPaymentPRReg_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmCarPaymentPRReg_Activated")
        If pnLoadx = 1 Then
            Me.Text = "Car Payment Reg (PR)"

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

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
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
                        poTrans.LoanType = 1
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

    Private Sub frmCarPaymentPRReg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmCarPaymentPRReg_Load")
        If pnLoadx = 0 Then
            poTrans = New ggcLRTransaction.LRPayment_PR_Car(p_oAppDriver, -1)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf txtField_KeyDown)
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
                            Case 7, 8, 9, 10, 82, 86, 87, 91
                                If IsNumeric(poTrans.Master(loIndex)) And poTrans.Master(loIndex) <> vbEmpty Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                        txtOther10.Text = Format(CDbl(poTrans.Master("nRebatesx")), xsDECIMAL)
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls

        txtStatus.Text = getStatus(IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master("cPostedxx")))

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
            Case 3 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))

                If loIndex = 0 Then
                    poTrans.SearchBranch(poControl.Text, False, True)
                ElseIf loIndex = 1 Or loIndex = 2 Then
                    poTrans.LoanType = 1
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

    Private Sub poTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles poTrans.MasterRetrieved
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 80
                loTxt.Text = Value
                txtField04.Text = poTrans.Master(4)
                txtField81.Text = poTrans.Master(81)

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
            Case 7, 8, 9, 91
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 1
                loTxt.Text = Format(Value, xsDATE_MEDIUM)
            Case Else
                loTxt.Text = Value
        End Select
    End Sub

    Private Function getStatus(ByVal fcStatus As String) As String
        Select Case fcStatus
            Case "0"
                getStatus = "OPEN"
            Case "1"
                getStatus = "CLOSED"
            Case "2"
                getStatus = "POSTED"
            Case "3"
                getStatus = "CANCELLED"
            Case Else
                getStatus = "UNKNOWN"
        End Select
    End Function

End Class
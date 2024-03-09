Imports MySql.Data.MySqlClient
Imports ggcLRTransaction
Imports ggcAppDriver

Public Class frmAutoARReg
    Private WithEvents p_oTrans As ggcLRTransaction.LRMasterCarNeo
    Private pnLoadx As Integer
    Private poControl As Control
    Private p_nEditMode As Integer
    Private p_AccountStatus As String

    Public Property AccountStatus() As String
        Get
            Return p_AccountStatus
        End Get
        Set(ByVal value As String)
            p_AccountStatus = value
        End Set
    End Property

    Private Sub frmLoanView_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmAutoARReg_Activated")

        If p_AccountStatus = -1 Then
            Me.Text = "Auto AR Account- Active"
        Else : p_AccountStatus = 1
            Me.Text = "Auto AR Account- Closed"
        End If
        If pnLoadx = 1 Then
            p_oTrans.InitTransaction()
            p_oTrans.NewTransaction()
            Call newRecord()
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmLoanView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

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
                    Case 0, 1
                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 0, True, False)) = True Then
                            loadMaster()
                            setTextSeeks()
                        End If
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Public Function setTextSeeks() As Boolean
        txtSeeks00.Text = p_oTrans.Master("sAcctNmbr")
        txtSeeks01.Text = txtField02.Text
        Return True
    End Function

    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Right
                        SetNextFocus()
                    Case Keys.Up, Keys.Left
                        SetPreviousFocus()
                End Select
        End Select
    End Sub

    Private Sub frmLoanView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLoanView_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcLRTransaction.LRMasterCarNeo(p_oAppDriver)
            p_oTrans.AccountStatus = p_AccountStatus
            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)
            Call grpKeyHandler(Me, GetType(TextBox), "txtSeeks", "KeyDown", AddressOf frmLoanView_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf frmLoanView_KeyDown)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)
            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster()
        Call loadEntry(Me.Panel1)
        Call loadEntry(Me.Panel3)
        TabControl1.SelectedIndex = 0
    End Sub

    Private Function newRecord() As Boolean
        Call loadMaster()
        txtOther01.Text = "0.00"
        txtOther02.Text = "0.00"
        Return True
    End Function

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadEntry(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case 1, 10, 12, 16
                            If IsDate(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), "MMMM dd, yyyy")
                            End If
                        Case 5 To 9, 13, 14, 15, 18 To 23, 25, 29, 30
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                loTxt.Text = Format(p_oTrans.Master(loIndex), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 24
                            If IsNumeric(p_oTrans.Master(loIndex)) Then
                                'loTxt.Text = p_oTrans.Master("nPrincipl") + p_oTrans.Master("nInterest")
                                'loTxt.Text = CDec(loTxt.Text) - ((p_oTrans.Master("nPrincipl") - IFNull(p_oTrans.Master("nABalance"), 0)) + IFNull(p_oTrans.Master("nIntTotal"), 0))
                                loTxt.Text = p_oTrans.Master("nABalance")
                                loTxt.Text = Format(CDec(loTxt.Text), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 31
                            loTxt.Text = IIf(p_oTrans.Master("cActivexx") = "1", "ACTIVE", "INACTIVE")
                        Case 27
                            loTxt.Text = IFNull(p_oTrans.Master("cAcctStat"), "UNKNOWN")
                            If loTxt.Text = "0" Then
                                loTxt.Text = "ACTIVE"
                            End If
                        Case 34
                            loTxt.Text = IFNull(p_oTrans.Detail("sEngineNo"), "")
                        Case 35
                            loTxt.Text = IFNull(p_oTrans.Detail("sFrameNox"), "")
                        Case 36
                            loTxt.Text = IFNull(p_oTrans.Detail("sModelNme"), "")
                        Case 37
                            loTxt.Text = IFNull(p_oTrans.Detail("sBrandNme"), "")
                        Case 38
                            loTxt.Text = IFNull(p_oTrans.Detail("sColorNme"), "")
                        Case 39
                            loTxt.Text = IFNull(p_oTrans.Detail("sFileNoxx"), "")
                        Case 40
                            loTxt.Text = IFNull(p_oTrans.Detail("sCRENoxxx"), "")
                        Case 41
                            loTxt.Text = IFNull(p_oTrans.Detail("sCRNoxxxx"), "")
                        Case 42
                            loTxt.Text = IFNull(p_oTrans.Detail("sPlateNoP"), "")
                        Case 43
                            loTxt.Text = IFNull(p_oTrans.Detail("dRegister"), Format(p_oAppDriver.getSysDate, "MMMM dd, yyyy"))
                            If Not loTxt.Text = "" Then
                                p_oTrans.Detail("dRegister") = loTxt.Text
                                loTxt.Text = Format(p_oTrans.Detail("dRegister"), "MMMM dd, yyyy")
                            End If
                        Case 44
                            loTxt.Text = IFNull(p_oTrans.Detail("nYearModl"), Format(p_oAppDriver.getSysDate, "yyyy"))
                        Case 46
                            If IFNull(p_oTrans.Detail("nSubsidze")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nSubsidze"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 47
                            If IFNull(p_oTrans.Detail("nSubCrdtd")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nSubCrdtd"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 48
                            If IFNull(p_oTrans.Detail("nInctvAmt")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInctvAmt"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 49
                            If IFNull(p_oTrans.Detail("nIncntPdx")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nIncntPdx"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 50
                            If IFNull(p_oTrans.Detail("nInsAmtxx")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInsAmtxx"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case 51
                            If IFNull(p_oTrans.Detail("nInsAmtPd")) Then
                                loTxt.Text = Format(p_oTrans.Detail("nInsAmtPd"), xsDECIMAL)
                            Else
                                loTxt.Text = "0.00"
                            End If
                        Case Else
                            loTxt.Text = IFNull(p_oTrans.Master(loIndex), "")
                    End Select
                End If
                txtOther02.Text = Format(Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")), xsDECIMAL)
                'txtOther01.Text = Format(Val(p_oTrans.Master("nMonAmort")), xsDECIMAL)

                Dim lsMon As String
                lsMon = Format(Math.Round(Val(p_oTrans.Master("nMonAmort")) + Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")) + Val(p_oTrans.Master("nRebatesx"))), xsDECIMAL)
                'txtOther01.Text = Format(Val(p_oTrans.Master("nMonAmort")) + Val(p_oTrans.Master("nInterest")) / Val(p_oTrans.Master("nAcctTerm")) + Val(p_oTrans.Master("nRebatesx")), "#,##0.00")
                txtOther01.Text = lsMon

                'txtOther01.Text = Format(Math.Floor(Val(p_oTrans.Master("nMonAmort")) + Val(p_oTrans.Master("nInterest")) + Val(p_oTrans.Master("nRebTotlx")) / Val(p_oTrans.Master("nAcctTerm"))), xsDECIMAL)
                'mac 2020.08.28
                '   display PN Value
                '   PNValue = Principal Amount + Interest Amount
                txtOther03.Text = Format(p_oTrans.Master("nPrincipl") + p_oTrans.Master("nInterest"), xsDECIMAL)
            End If
        Next
    End Sub

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

    ''Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            p_oTrans.Master(loIndex) = loTxt.Text
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
            Case 2 ' Browse
                If p_oTrans.SearchTransaction("", False) = True Then
                    loadMaster()
                    p_nEditMode = xeEditMode.MODE_READY
                    setTextSeeks()
                End If

            Case 5 ' Ledger
                Dim loFrm As New frmAutoLedger
                loFrm.AccountNo = p_oTrans.Master("sAcctNmbr")
                loFrm.ClientName = p_oTrans.Master("sClientNm")
                loFrm.Address = IFNull(p_oTrans.Master("xAddressx"), "")
                loFrm.CarModel = IFNull(p_oTrans.Detail("sModelNme"), "")
                loFrm.PlateNo = IFNull(p_oTrans.Detail("sPlateNoP"), "")
                loFrm.Interest = IFNull(p_oTrans.Master("nInterest"), 0)
                loFrm.Principal = IFNull(p_oTrans.Master("nPrincipl"), 0)
                loFrm.ShowDialog()
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
        loTxt.BackColor = SystemColors.Window

    End Sub

    Private Sub poTrans_MasterRetrieved(ByVal Index As Integer, ByVal Value As Object) Handles p_oTrans.MasterRetrieved
        Dim loTxt As TextBox
        'ind TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)
        Select Case Index
            Case 43
                loTxt.Text = Format(CDate(Value), "MMMM dd, yyyy")
            Case Else
                loTxt.Text = IFNull(Value, "")
        End Select
    End Sub
End Class
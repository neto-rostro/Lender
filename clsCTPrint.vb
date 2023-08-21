Imports MySql.Data.MySqlClient
Imports ADODB
Imports ggcAppDriver
Imports CrystalDecisions.CrystalReports.Engine

Public Class clsCTPrint
    Private p_oDriver As ggcAppDriver.GRider
    Private p_oSTRept As DataSet
    Private p_oDTSrce As DataTable

    Private p_nReptType As Integer      '0=Summary;1=Detail
    Private p_abInclude(1) As Integer   '0=Default;1=Financing
    Private p_sTransNox As String
    Private p_dTransact As Date
    Private p_sBranchCD As String

    Public WriteOnly Property Transaction
        Set(ByVal value)
            p_sTransNox = value
        End Set
    End Property

    Public WriteOnly Property TranDate
        Set(ByVal value)
            p_dTransact = value
        End Set
    End Property

    Public Function ReportTrans() As Boolean
        Dim oProg As frmProgress

        Dim lsSQL As String 'whole statement

        'Show progress bar
        oProg = New frmProgress
        oProg.PistonInfo = p_oDriver.AppPath & "/piston.avi"
        oProg.ShowTitle("EXTRACTING RECORDS FROM DATABASE")
        oProg.ShowProcess("Please wait...")
        oProg.Show()

        lsSQL = "SELECT" & _
                    " a.sReferNox 'sAcctNmbr'" & _
                    ", a.cBillType 'cBillType'" & _
                    ", a.nAmountxx 'nAmountxx'" & _
                    ", b.nPrincipl 'nPrincipl'" & _
                    ", b.nInterest 'nInterest'" & _
                    ", CONCAT(c.sLastName, ', ', c.sFrstName, ' ', c.sMiddName) sClientNm" & _
                    ", d.nSubsidze 'nSubsidze'" & _
                    ", d.nInctvAmt 'nInctvAmt'" & _
                    ", e.sEngineNo 'sEngineNo'" & _
                " FROM  CT_Billing_Detail a" & _
                    ", LR_Master b" & _
                    ", Client_Master c" & _
                    ", LR_Master_Car d" & _
                    ", Car_Serial e" & _
                " WHERE a.sReferNox = b.sAcctNmbr" & _
                    " AND b.sClientID = c.sClientID" & _
                    " AND b.sAcctNmbr = d.sAcctNmbr" & _
                    " AND d.sSerialID = e.sSerialID" & _
                    " AND a.sTransNox = " & strParm(p_sTransNox) & _
                " ORDER BY a.nEntryNox ASC"


        If lsSQL <> "" Then
            lsSQL = lsSQL & ""
        End If

        p_oDTSrce = p_oDriver.ExecuteQuery(lsSQL)

        Dim loDtaTbl As DataTable = getRptTable()
        Dim lnCtr As Integer

        oProg.ShowTitle("LOADING RECORDS")
        oProg.MaxValue = p_oDTSrce.Rows.Count

        For lnCtr = 0 To p_oDTSrce.Rows.Count - 1

            oProg.ShowProcess("Loading " & p_oDTSrce(lnCtr).Item("sClientNm") & "...")

            loDtaTbl.Rows.Add(addRow(lnCtr, loDtaTbl))
        Next

        oProg.ShowSuccess()

        Dim clsRpt As clsBillPrint
        clsRpt = New clsBillPrint
        clsRpt.GRider = p_oDriver
        'Set the Report Source Here
        If Not clsRpt.initReport("TLMC1") Then
            Return False
        End If

        Dim loRpt As ReportDocument = clsRpt.ReportSource

        Dim loTxtObj As CrystalDecisions.CrystalReports.Engine.TextObject
        loTxtObj = loRpt.ReportDefinition.Sections(0).ReportObjects("txtCompany")
        loTxtObj.Text = p_oDriver.BranchName

        'Set Branch Address
        loTxtObj = loRpt.ReportDefinition.Sections(0).ReportObjects("txtAddress")
        loTxtObj.Text = p_oDriver.Address & vbCrLf & p_oDriver.TownCity & " " & p_oDriver.ZippCode & vbCrLf & p_oDriver.Province

        'Set First Header
        loTxtObj = loRpt.ReportDefinition.Sections(1).ReportObjects("txtHeading1")
        loTxtObj.Text = "Car Trade Transaction Summary"

        'Set Second Header
        loTxtObj = loRpt.ReportDefinition.Sections(1).ReportObjects("txtHeading2")
        loTxtObj.Text = Format(p_dTransact, "MMMM dd yyyy")

        loTxtObj = loRpt.ReportDefinition.Sections(3).ReportObjects("txtRptUser")
        loTxtObj.Text = Decrypt(p_oDriver.UserName, "08220326")

        loRpt.SetDataSource(p_oSTRept)
        clsRpt.showReport()

        Return True
    End Function

    Private Function getRptTable() As DataTable
        'Initialize DataSet
        p_oSTRept = New DataSet

        'Load the data structure of the Dataset
        'Data structure was saved at DataSet1.xsd 
        p_oSTRept.ReadXmlSchema(p_oDriver.AppPath & "\vb.net\Reports\DataSet1.xsd")

        'Return the schema of the datatable derive from the DataSet 
        Return p_oSTRept.Tables(0)
    End Function

    Private Function addRow(ByVal lnRow As Integer, ByVal foSchemaTable As DataTable) As DataRow
        'ByVal foDTInclue As DataTable
        Dim loDtaRow As DataRow

        'Create row based on the schema of foSchemaTable
        loDtaRow = foSchemaTable.NewRow

        loDtaRow.Item("nField01") = lnRow + 1
        loDtaRow.Item("sField01") = p_oDTSrce(lnRow).Item("sAcctNmbr")
        loDtaRow.Item("sField02") = p_oDTSrce(lnRow).Item("sClientNm")
        loDtaRow.Item("sField03") = p_oDTSrce(lnRow).Item("sEngineNo")
        loDtaRow.Item("sField04") = p_oDTSrce(lnRow).Item("cBillType")
        loDtaRow.Item("lField01") = p_oDTSrce(lnRow).Item("nPrincipl")
        loDtaRow.Item("lField02") = p_oDTSrce(lnRow).Item("nInterest")
        loDtaRow.Item("lField03") = p_oDTSrce(lnRow).Item("nSubsidze")
        loDtaRow.Item("lField04") = p_oDTSrce(lnRow).Item("nInctvAmt")
        loDtaRow.Item("lField05") = p_oDTSrce(lnRow).Item("nAmountxx")

        If p_oDTSrce(lnRow).Item("cBillType") = "0" Then
            loDtaRow.Item("sField04") = "Principal/Finance"
        ElseIf p_oDTSrce(lnRow).Item("cBillType") = "1" Then
            loDtaRow.Item("sField04") = "Insurance Amount"
        ElseIf p_oDTSrce(lnRow).Item("cBillType") = "2" Then
            loDtaRow.Item("sField04") = "Dealer's Incentive"
        ElseIf p_oDTSrce(lnRow).Item("cBillType") = "3" Then
            loDtaRow.Item("sField04") = "Subsidized Interest"
        ElseIf p_oDTSrce(lnRow).Item("cBillType") = "4" Then
            loDtaRow.Item("sField04") = "Adjustment"
        End If

        Return loDtaRow
    End Function

    Public Sub New(ByVal foRider As GRider)
        p_oDriver = foRider
        p_oSTRept = Nothing
        p_oDTSrce = Nothing
    End Sub

End Class

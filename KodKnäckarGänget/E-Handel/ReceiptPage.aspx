<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptPage.aspx.cs" Inherits="E_Handel.ReceiptPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/Receipt.css" rel="stylesheet" />
</head>
<body>
    <form id="ReceiptForm" runat="server">
        <div class="container-fluid">
            <div class="row-fluid">
                <div id="ReceiptOutput">

                    <div>
                    <asp:Panel ID="ReceiptPanel" runat="server">
                    </asp:Panel>
                    </div>
                        
                    <div id="ReceiptOrderId">
                        
                                <label id="OrderIdlbl" runat="server">Your order id is: </label>
                    </div>

                    <div id="ReceiptTotalPrice">

                        <label id="TotalPricelbl" runat="server">Total price: </label>
                    </div>

                    <div id="ReceiptVAT">

                        <label id="VATlbl" runat="server">Value added tax: </label>
                    </div>

                    <div id="ReceiptPostage">

                        <label id="Postagelbl" runat="server">Postage: </label>
                    </div>

                    <div id="ReceiptDeliveryAdress">

                        <label id="Addresslbl" runat="server">Delivery Adress: </label>
                    </div>

                    <div id="ReceiptPostalCode">

                        <label id="PostalCodelbl" runat="server">Postal Code: </label>
                    </div>

                    <div id="ReceiptCity">

                        <label id="Citylbl" runat="server">City: </label>
                    </div>

                    <div id="ReceiptCountry">

                        <label id="Countrylbl" runat="server">Country: </label>
                    </div>

                    <div id="ReceiptEmail">

                        <label id="Emaillbl" runat="server">Email adress: </label>
                    </div>

                    <div id="ReceiptPhoneNumber">

                        <label id="TelephoneNumberlbl" runat="server">Phone number: </label>
                    </div>

                    <div id="ReceiptPaymentOptions">

                        <label id="PaymentOptionslbl" runat="server">Payment options: </label>
                    </div>

                    <div id="ReceiptDeliveryOption">

                        <label id="Deliveryoptionslbl" runat="server">Delivery options: </label>
                    </div>
                    

                    <div id="receiptPageButtons">
                    <asp:Button ID="receiptHomeButton" runat="server" text="Return to shop" OnClick="receiptHomeButton_Click"/>     
                    <asp:Button ID="receiptPrintButton" runat="server" text="Print receipt" OnClick="receiptPrintButton_Click" />   
                    </div>
                    

                </div>

            </div>
        </div>
    </form>
</body>
</html>
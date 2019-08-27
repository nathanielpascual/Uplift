var dataTable;

$(document).ready(() => {
    var url = window.location.search;

    if (url.includes("approved")) {
        loadDataTable("GetAllApprovedOrders");
    }
    else if (url.includes("pending"))  {
        loadDataTable("GetAllPendingOrders");
    }
    else {
        loadDataTable("GetAllOrders");
    }
});

loadDataTable=(url)=> {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/" + url,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "serviceCount", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render":  (data)=> {
                    return `<div class="text-center">
                                <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px'>
                                    <i class='far fa-edit'></i>&nbsp;Details
                                </a>
                            </div>
                           `;
                },"width" : "20%"
            }
        ],
        "language": {
            "emptyTable" : "No records found"
        },
        "width" : "100%"
    });
}

 


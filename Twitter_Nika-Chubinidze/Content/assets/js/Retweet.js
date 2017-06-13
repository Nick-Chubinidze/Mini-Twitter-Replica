$(document).ready(function () {
    $("input[type='text']").keyup(function () { 
        var vv = $('input[type="text"]').filter(function () {
            return this.value.length > 0
        }).first().val() 

        if (vv != undefined) {
            if (vv.length > 0) {
                $(":button").not("#btnedit,#btnsave,#btncancel,#btnButton").prop('disabled', false);
            }
        } else {
            $(":button").not("#btnedit,#btnsave,#btncancel,#btnButton").prop('disabled', true);
        }

        $(":button").each(function () {  
            if (this.name == "btnDelete") {
                this.disabled=false 
            }
        }) 
    })
})

function updateSuccessTweet() {
    this.reset()
    $("#txtComment").val('')
    $(":button").not("#btnedit,#btnsave,#btncancel,#btnButton").prop('disabled', true);

    $(":button").each(function () {
        if (this.name == "btnDelete") {
            this.disabled = false
        }
    })
}
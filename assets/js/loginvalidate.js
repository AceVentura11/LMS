$(function () {
    $("#form1").validate(
    {
        rules:
        {
            txt_uname:
            {
                required: true,
                minlength: 2,
                maxlength: 150
            },
            txt_password: {
                required: true,
                minlength: 2,
                maxlength: 150
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        }
    });

    $("#form2").validate(
    {
        rules:
        {
            txt_uname:
            {
                required: true,
                minlength: 2,
                maxlength: 150
            },
            txt_oldpassword: {
                required: true,
                minlength: 2,
                maxlength: 150
            },
            txt_password: {
                required: true,
                minlength: 2,
                maxlength: 150
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        }
    });
});
$('.logout-confirm').confirm({
    icon: 'fa fa-power-off',
    title: 'Logout?',
    theme: 'bootstrap',
    animation: 'right',
    closeAnimation: 'right',
    backgroundDismiss: true,
    closeIcon: true,
    type: 'blue',
    buttons: {

        Yes: {
            text: 'yes',
            btnClass: 'btn-primary',
            action: function () {
                location.href = this.$target.attr('href');
            },
        },
        no: {
            text: 'no',
            action: function () {
                //
            }
        }
    }

});
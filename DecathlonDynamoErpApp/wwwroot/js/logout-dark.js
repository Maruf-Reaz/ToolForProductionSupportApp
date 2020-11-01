$('.logout-confirm').confirm({
    icon: 'fa fa-power-off',
    title: 'Logout?',
    theme: 'supervan',
    animation: 'zoom',
    closeAnimation: 'zoom',
    backgroundDismiss: true,
    closeIcon: true,
    type: 'orange',
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
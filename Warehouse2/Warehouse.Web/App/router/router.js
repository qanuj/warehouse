/* Setup Rounting For All Pages */
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    var role = document.querySelector('html').dataset.role.toLowerCase();
    var common = role == "admin" ? "admin" : "common";

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/");
    var datePickerDependency = {};

    var searchDependency = {};

    $stateProvider
        .state('profile', {
            url: "/profile",
            templateUrl: "app/views/" + role + "/profile.html",
            data: { pageTitle: 'Profile' },
            controller: role + "ProfileController",
            resolve: {}
        })
        .state('billing', {
            url: "/billing",
            templateUrl: "app/views/" + common + "/billing.html",
            data: { pageTitle: 'Billing' },
            controller:  role=="admin" ? "adminBillingController" : "billingController",
            resolve: {}
        })
        .state('invoice', {
            url: "/invoice/:id",
            templateUrl: "app/views/" + common + "/invoice.html",
            data: { pageTitle: 'Invoice' },
            controller: role=="admin" ? "adminInvoiceController" : "invoiceController",
            resolve: {}
        })
        .state('credit', {
            url: "/billing/credit",
            templateUrl: "app/views/" + common + "/addCredit.html",
            data: { pageTitle: 'Add Credit' },
            controller: role == "admin" ? "adminCreditController" : "creditController",
            resolve: {}
        })
        .state('profileEdit', {
            url: "/profile/edit",
            templateUrl: "app/views/" + role + "/editProfile.html",
            data: { pageTitle: 'Edit Profile' },
            controller: role + "EditProfileController",
            resolve: {}
        });

    if (role == 'member') {
    } else if (role == 'admin') {
        $stateProvider
            .state('country', {
                url: "/country",
                templateUrl: "app/views/" + role + "/master.html",
                data: { pageTitle: 'Countries', module: 'country' },
                controller: role + "MasterController",
                resolve: {}
            })
            .state('invite', {
                url: "/schedule",
                templateUrl: "app/views/" + role + "/invite.html",
                data: { pageTitle: 'Invite' },
                controller: role + "InviteController",
                resolve: {}
            })
            .state('feedback', {
                url: "/inbox/feedback",
                templateUrl: "app/views/" + role + "/inbox.html",
                data: { pageTitle: 'site feedback',module:'feedback' },
                controller: role + "InboxController",
                resolve: {}
            })
            .state('banner', {
                url: "/banner",
                templateUrl: "app/views/" + role + "/banner.html",
                data: { pageTitle: 'Banners',module:'banner' },
                controller: role + "BannerController",
                resolve: {}
            })
            .state('config', {
                url: "/config",
                templateUrl: "app/views/" + role + "/config.html",
                data: { pageTitle: 'Configuration' },
                controller: role + "ConfigController",
                resolve: {}
            })
            .state('config2', {
                url: "/",
                templateUrl: "app/views/" + role + "/config.html",
                data: { pageTitle: 'Configuration' },
                controller: role + "ConfigController",
                resolve: {}
            });
    }

}]);
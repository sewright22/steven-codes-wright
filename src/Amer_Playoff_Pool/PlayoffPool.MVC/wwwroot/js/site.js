// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var Common = Common || {};

Common = function () {
    this.initialize = function (parentElement) {
        document.addEventListener("DOMContentLoaded", function () {
            $("button.modal-button").each(function (index, element) {
                element.addEventListener("click", function (event) {
                    // Get url from data-url attribute
                    var url = element.getAttribute("data-url");

                    // Call ajax function
$.ajax({
                        url: url,
                        type: "GET",
                        success: function (data) {
                            // Set modal body html
                            $("#site-modal").html(data);

                            var myModalEl = document.getElementById('staticBackdrop')
                            myModalEl.addEventListener('hidden.bs.modal', function (event) {
                                $("#site-modal").html('')
                            })

                            // Show modal
                            var myModal = new bootstrap.Modal(myModalEl, {
                                keyboard: false
                            });
                            myModal.show();
                        },
                        error: function (xhr, status, error) {
                            // Show error
                            $("#site-modal .modal-body").html(xhr.responseText);

                            // Show modal
                            var modalDiv = document.getElementById("site-modal");
                            modal.style.display = "block";
                        }
                    });
                });
            });
        });
    }
}

var common = new Common();
common.initialize(document.documentElement);
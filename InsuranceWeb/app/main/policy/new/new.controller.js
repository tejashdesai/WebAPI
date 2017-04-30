(function () {
    'use strict';
    angular.module('insuranceApp.main.policy').controller('NewPolicyController', newPolicyController);

    newPolicyController.$inject = ['$uibModal', '$state', '$stateParams', '$filter', 'PolicyService', 'message'];

    function newPolicyController($uibModal, $state, $stateParams, $filter, PolicyService, message) {
        var vm = this;

        vm.emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
        vm.mobilePattern = "/^[0-9]{10,10}$/";

        vm.policyId = $stateParams.policyId;

        vm.type = {
            old: 0,
            deleted: 1,
            new: 2
        };

        vm.date = {
            endDate: null,
            startDate: null
        };

        vm.open = {
            endDate: false,
            startDate: false
        };

        vm.allDateOption = {
            showWeeks: false
        };

        vm.endDateOption = {
            minDate: new Date(),
            showWeeks: false
        };

        function initDTO() {
            vm.uploadedFiles = [];
            vm.tempUploadedFiles = [];
            vm.fileError = {};
            vm.file = {};
            vm.policyData = {};
        }

        function setDTOData() {
            var temp = $filter('filter')(vm.policyData.policyHistory, {
                'isCurrent': true
            }, true);
            vm.date.startDate = new Date(temp[0].startDate);
            vm.date.endDate = new Date(temp[0].endDate);
        }

        function getPolicyData() {
            PolicyService.getPolicyData(vm.policyId, function (data) {
                vm.policyData = data;
                setDTOData();
            }, function (error) {
            })
        }

        function policyHistoryPopup(item,isEdit) {
            var modalInstance = $uibModal.open({
                templateUrl: 'policyHistoryModal.html',
                controller: 'PolicyHistoryPopupController',
                controllerAs: 'history',
                resolve: {
                    items: function () {
                        return angular.copy(item);
                    }
                }
            });

            modalInstance.result.then(function (historyData) {
                if (isEdit) {
                    var index = vm.policyData.policyHistory.indexOf(item);
                    vm.policyData.policyHistory[index] = historyData;
                } else {
                    vm.policyData.policyHistory.push(historyData);
                }
            }, function () {

            });
        }

        function init() {
            initDTO();
            if (!angular.isEmpty(vm.policyId)) {
                getPolicyData();
            }
        }

        vm.openDate = function (key) {
            vm.open[key] = true;
        };

        vm.startDateChanged = function () {
            if (angular.isNull(vm.date.startDate)) {
                vm.date.endDate = null;
                vm.endDateOption.minDate = new Date();
            } else {
                vm.date.endDate = null;
                vm.endDateOption.minDate = angular.copy(new Date(vm.date.startDate));
            }
        };

        vm.addFile = function () {
            if (vm.fileError.invalid) {
                return;
            }
            if (vm.tempUploadedFiles.length === 0) {
                vm.fileError = {
                    message: 'Select file',
                    invalid: true
                }
                return;
            }
            vm.file.fileData = vm.tempUploadedFiles[0];
            vm.file.type = vm.type.new;
            vm.file.name = vm.file.fileData.name;
            vm.uploadedFiles.push(vm.file);
            vm.file = {};
            vm.tempUploadedFiles = [];
            angular.element('#fileUpload').val('');
        };

        vm.removeFile = function (item) {
            if (item.type === vm.type.old) {
                item.type = vm.type.deleted;
            } else {
                var index = vm.uploadedFiles.indexOf(item);
                vm.uploadedFiles.splice(index, 1);
            }
        };

        vm.savePolicy = function () {
            var sendData = new FormData();

            vm.policyData.startDate = vm.date.startDate;
            vm.policyData.endDate = vm.date.endDate;

            sendData.append('policy', angular.toJson(vm.policyData));

            var newDocuments = $filter('filter')(vm.uploadedFiles, {
                type: vm.type.new
            });

            for (var i = 0; i < newDocuments.length; i++) {
                sendData.append('files', newDocuments[i].fileData, newDocuments[i].fileData.name);
            }

            // var oldDocuments = $filter('filter')(vm.uploadedFiles, {
            //     type: vm.type.old
            // });
            // if (oldDocuments.length > 0) {
            //     sendData.append('fileHoldNames', _.map(oldDocuments, 'name').join(','));
            // }

            var deletedDocuments = $filter('filter')(vm.uploadedFiles, {
                type: vm.type.deleted
            });
            if (deletedDocuments.length > 0) {
                sendData.append('deletedFiles', _.map(deletedDocuments, 'name').join(','));
            }

            PolicyService.savePolicy(sendData, function (data) {
                message.success('Policy','Saved Successfully');
                $state.go('main.policy.current');
            }, function (error) {
                message.error('Policy','Error while save policy.');
            })
        };

        vm.resetPolicy = function () {
            init();
        };

        vm.addPolicyHistory = function () {
            policyHistoryPopup({
                policyID: vm.policyData.policyID,
                isDeleted: false
            });
        };

        vm.editPolicyHistory = function (item) {
            policyHistoryPopup(item,true);
        };

        vm.deletePolicyHistory = function (item) {
            item.isDeleted = true;
        };

        init();
    }
})();

(function () {
    'use strict';
    angular.module('insuranceApp.main.policy').controller('PolicyHistoryPopupController', policyHistoryPopupController);

    policyHistoryPopupController.$inject = ['$uibModalInstance', 'items'];

    function policyHistoryPopupController($uibModalInstance, items) {

        var vm = this;
        vm.policyHistoryData = items;

        vm.open = {
            endDate: false,
            startDate: false
        };

        vm.allDateOption = {
            showWeeks: false
        };

        vm.endDateOption = {
            minDate: new Date(),
            showWeeks: false
        };

        function setDTO(){
            if(vm.policyHistoryData.policyHistoryID){
                vm.policyHistoryData.startDate = new Date(vm.policyHistoryData.startDate);
                vm.policyHistoryData.endDate = new Date(vm.policyHistoryData.endDate);
            }
        }

        vm.openDate = function (key) {
            vm.open[key] = true;
        };

        vm.startDateChanged = function () {
            if (angular.isNull(vm.policyHistoryData.startDate)) {
                vm.policyHistoryData.endDate = null;
                vm.endDateOption.minDate = new Date();
            } else {
                vm.policyHistoryData.endDate = null;
                vm.endDateOption.minDate = angular.copy(new Date(vm.policyHistoryData.startDate));
            }
        };

        vm.savePolicyHistory = function () {
            $uibModalInstance.close(vm.policyHistoryData);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss();
        }

        setDTO();
    }

})();
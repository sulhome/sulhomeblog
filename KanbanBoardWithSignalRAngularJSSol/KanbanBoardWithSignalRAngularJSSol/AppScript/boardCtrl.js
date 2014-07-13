app.kanbanBoardApp.controller('boardCtrl', function ($scope, $modal, boardService) {
    $scope.columns = [];        

    $scope.refreshBoard = function refreshBoard() {
        $scope.setLoading(true);
        boardService.getColumns()
           .then(function (data) {
               $scope.setLoading(false);
               $scope.columns = data;
           }, function (error) {
               $scope.setLoading(false);
               console.log(error);
           });        
    };

    function init() {        
        $scope.setLoading(true);        
        boardService.initialize().then(function (data) {
            $scope.setLoading(false);
            $scope.refreshBoard();
        }, function () {
            $scope.setLoading(false);            
        });
        
    };   

    $scope.onDrop = function ($event, $data, targetCol) {        
        boardService.canMoveTask($data.ColumnId, targetCol.Id)
            .then(function (canMove) {
                canMove = angular.fromJson(canMove);
                if (canMove) {                 
                    boardService.moveTask($data.Id, targetCol.Id).then(function (data) {
                        $scope.setLoading(false);
                        $scope.refreshBoard();
                        boardService.sendRequest();
                    }, function (error) {
                        $scope.setLoading(false);
                        console.log(error);
                    });
                    $scope.setLoading(true);                   
                }

            }, function (error) {
                console.log(error);
            });
    };

    $scope.busyIndicatorOpened = false;

    $scope.setLoading = function (loadingFlag) {
        if (loadingFlag && !$scope.busyIndicatorOpened) {
            $scope.busyIndicatorOpened = false;
            $scope.modalInstance =
            $modal.open({
                templateUrl: '/AppScript/busymodal.html',
                backdrop: 'static',
                keyboard: false
            });
            $scope.busyIndicatorOpened = true;
        }
        else if (!loadingFlag && $scope.busyIndicatorOpened) {
            if ($scope.modalInstance) {
                $scope.modalInstance.dismiss('cancel');
                $scope.busyIndicatorOpened = false;
            }
        }
    }

    $scope.$parent.$on("refreshBoard", function (e) {
        $scope.refreshBoard();        
    });

    init();
});
app.kanbanBoardApp.controller('boardCtrl', function ($scope, $modal, boardService) {
    $scope.columns = [];        
    $scope.isLoading = false;

    $scope.refreshBoard = function refreshBoard() {        
        $scope.isLoading = true;
        boardService.getColumns()
           .then(function (data) {               
               $scope.isLoading = false;
               $scope.columns = data;
           }, onError);
    };

    function init() {        
        $scope.isLoading = true;
        boardService.initialize().then(function (data) {
            $scope.isLoading = false;
            $scope.refreshBoard();
        }, function () {
            $scope.isLoading = false;
        });
        
    };   

    $scope.onDrop = function ($event, $data, targetCol) {        
        boardService.canMoveTask($data.ColumnId, targetCol.Id)
            .then(function (canMove) {                
                if (canMove) {                 
                    boardService.moveTask($data.Id, targetCol.Id).then(function (taskMoved) {
                        $scope.isLoading = false;
                        $scope.refreshBoard();
                        boardService.sendRequest();
                    }, onError);
                    $scope.isLoading = true;
                }

            }, onError);
    };

    $scope.busyIndicatorOpened = false;    

    $scope.$parent.$on("refreshBoard", function (e) {
        $scope.refreshBoard();        
    });

    var onError = function (error) {
        $scope.isLoading = false;
        console.log(error);
    };

    init();
});
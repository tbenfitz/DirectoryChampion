ko.bindingHandlers.fadeVisible = {
   init: function (element, valueAccessor) {
      var shouldDisplay = valueAccesser();
      $(element).toggle(shouldDisplay);
   },
   update: function (element, valueAccessor) {
      var shouldDisplay = valueAccessor();
      shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
   }
};
import { makeAutoObservable, action } from 'mobx'

class BasePopperStore {

    anchor = null;
    
    open = Boolean(this.anchor);

    hidden = "visible";

    visible = "visible";

    min = false;

    constructor() {
        makeAutoObservable(this,
            {

            });
    }

    Minimize(min) {
        this.min = min;
    }

    SetAnchor(event) {

        this.anchor = event;

        this.open = Boolean(this.anchor);
    }


    HideShowComponents() {

        this.hidden = "hidden";
        this.visible = "visible";
    }

  }


export default BasePopperStore;
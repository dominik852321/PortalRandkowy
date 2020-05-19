import { UserEditComponent } from '../user/user-edit/user-edit.component';
import { CanDeactivate } from '@angular/router';

export class PreventUnsavedChanges implements CanDeactivate<UserEditComponent> {
    canDeactivate(component: UserEditComponent) {
        if(component.editForm.dirty) {
            return confirm('Nie zapisałeś zmian');
        }
        return true;
    }
}
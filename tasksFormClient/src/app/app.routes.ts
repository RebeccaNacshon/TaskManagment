import { Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AddTaskComponentComponent } from './add-task-component/add-task-component.component';
import { TaskListComponent } from './task-list/task-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


export const routes: Routes = [
    { path: 'add-task', component: AddTaskComponentComponent},
    { path: 'task-list', component: TaskListComponent },
    { path: '', redirectTo: '/task-list', pathMatch: 'full' }
  ];
  
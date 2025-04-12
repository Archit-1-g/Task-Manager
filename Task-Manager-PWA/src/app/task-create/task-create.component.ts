import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from '../auth/task.service';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.css']
})
export class TaskCreateComponent {
  task: any = {
    title: '',
    description: '',
    dueDate: '',
    status: 'Pending',
    remarks: '',
    createdById: 1,  // this should come from logged-in user's data (auth)
    createdByName: 'Admin',  // or get from auth
    createdOn: new Date().toISOString(), // Setting current date time
    lastUpdatedOn: new Date().toISOString(), // Setting current date time
    updatedById: 1, // Initial user or update logic can be managed by your auth system
    updatedByName: 'Admin'  // Or get from auth
  };

  constructor(private taskService: TaskService, private router: Router) {}

  createTask() {
    this.taskService.createTask(this.task).subscribe(
      () => {
        this.router.navigate(['/tasks']); // Redirect to task list page
      },
      error => {
        console.error('Error creating task:', error);
      }
    );
  }
}

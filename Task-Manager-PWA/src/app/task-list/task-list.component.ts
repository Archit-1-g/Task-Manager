import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TaskService } from '../auth/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  tasks: any[] = [];
  paginatedTasks: any[] = [];
  currentPage: number = 1;
  totalPages: number = 1;
  showModal: boolean = false;
  taskToDelete: any = null;
  searchQuery: string = '';

  constructor(private taskService: TaskService, private router: Router) {}

  ngOnInit(): void {
    this.getTasks();
  }

  getTasks(): void {
    this.taskService.getAllTasks().subscribe((tasks) => {
      this.tasks = tasks;
      this.totalPages = Math.ceil(this.tasks.length / 5);
      this.currentPage = 1;
      this.updateCurrentPageTasks();
    });
  }

  updateCurrentPageTasks(): void {
    const start = (this.currentPage - 1) * 5;
    const end = start + 5;
    this.paginatedTasks = this.tasks.slice(start, end);
  }

  searchTasks(): void {
    if (this.searchQuery.trim()) {
      this.taskService.searchTasks(this.searchQuery).subscribe((results) => {
        this.tasks = results;
        this.currentPage = 1;
        this.totalPages = Math.ceil(this.tasks.length / 5);
        this.updateCurrentPageTasks();
      });
    } else {
      this.getTasks();
    }
  }

  confirmDelete(task: any): void {
    this.taskToDelete = task;
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.taskToDelete = null;
  }

  deleteTask(): void {
    if (this.taskToDelete) {
      this.taskService.deleteTask(this.taskToDelete.id).subscribe(() => {
        this.getTasks();
        this.closeModal();
      });
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updateCurrentPageTasks();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateCurrentPageTasks();
    }
  }

  goToCreateTask(): void {
    this.router.navigate(['/create-task']);
  }

  goToEditTask(id: number): void {
    this.router.navigate(['/edit-task', id]);
  }

  goToViewTask(id: number): void {
    this.router.navigate(['/task', id]);
  }
}

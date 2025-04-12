import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../auth/task.service';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {
  task: any = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService
  ) {}

  ngOnInit(): void {
    const taskId = Number(this.route.snapshot.paramMap.get('id'));
    this.taskService.getTaskById(taskId).subscribe((data) => {
      this.task = data;
    });
  }

  updateTask() {
    this.taskService.updateTask(this.task.id, this.task).subscribe(() => {
      this.router.navigate(['/tasks']);
    });
  }
}

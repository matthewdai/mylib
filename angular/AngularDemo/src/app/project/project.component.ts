import { Component, Input } from '@angular/core';

@Component ({
    selector: 'project',
    templateUrl: 'project.component.html',
})

export class ProjectComponent {
    @Input() projectName : string;
}
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectCreationPageComponent } from './project-creation-page.component';

describe('ProjectCreationPageComponent', () => {
  let component: ProjectCreationPageComponent;
  let fixture: ComponentFixture<ProjectCreationPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectCreationPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectCreationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

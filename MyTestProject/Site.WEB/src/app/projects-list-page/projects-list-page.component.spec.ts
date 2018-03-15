import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectsListPageComponent } from './projects-list-page.component';

describe('ProjectsListPageComponent', () => {
  let component: ProjectsListPageComponent;
  let fixture: ComponentFixture<ProjectsListPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectsListPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectsListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

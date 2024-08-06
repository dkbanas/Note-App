import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteEditPageComponent } from './note-edit-page.component';

describe('NoteEditPageComponent', () => {
  let component: NoteEditPageComponent;
  let fixture: ComponentFixture<NoteEditPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoteEditPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NoteEditPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

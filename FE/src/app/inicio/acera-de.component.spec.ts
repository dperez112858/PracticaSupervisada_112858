import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AceraDeComponent } from './acera-de.component';

describe('AceraDeComponent', () => {
  let component: AceraDeComponent;
  let fixture: ComponentFixture<AceraDeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AceraDeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AceraDeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AltaInsumoComponent } from './alta-insumo.component';

describe('AltaInsumoComponent', () => {
  let component: AltaInsumoComponent;
  let fixture: ComponentFixture<AltaInsumoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AltaInsumoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AltaInsumoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});



!IFNDEF BIN_DIR
BIN_DIR=$(MAKEDIR)\bin
!ENDIF

# Sources and Binaries
EXE=$(BIN_DIR)
SRC=Server.cs Interfaces.cs

# Target Files
TARGETS=$(EXE)\ILS.Workflow.dll $(EXE)\Test.Workflow.exe
DELETE=$(TARGETS) $(EXE)\ILS.Workflow.pdb

# Rules

all: $(EXE) $(TARGETS)

$(EXE)\ILS.Workflow.dll: $(SRC) 
	$(CSC) /out:$@ /t:library $(CSC_FLAGS) /lib:$(EXE) $(SRC)

$(EXE)\Test.Workflow.exe: Test.cs
	$(CSC) /out:$@ /t:exe $(CSC_FLAGS) /r:$(TARGETS) /lib:$(EXE) Test.cs

install: all
uninstall:
clean: uninstall erase

!include $(MASTERMAK)
